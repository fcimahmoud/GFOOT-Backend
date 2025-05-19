
namespace Services.AgentServices
{
    public class EnvironmentalAgentService (IUnitOfWork unitOfWork, IEmailService emailService)
        : IEnvironmentalAgentService
    {
        async Task<IEnumerable<FactoryWithCarbonFootprintDto>> IEnvironmentalAgentService.GetAllFactoriesWithCarbonFootprintsAsync()
        {
            var factoryRepo = unitOfWork.GetRepository<FactoryUser, string>();
            var factories = await factoryRepo.GetAllWithIncludesAsync(f => true, f => f.ApplicationUser!, f => f.FactoryEmissions!);

            var factoryData = factories.Select(factory => new FactoryWithCarbonFootprintDto
            {
                FactoryId = factory.Id,
                FactoryName = factory.ApplicationUser?.DisplayName ?? "Unknown",
                TotalCarbonEmission = factory.FactoryEmissions!.OrderByDescending(f => f.Date).FirstOrDefault()?.CarbonEmission ?? 0
            });

            return factoryData;
        }

        public async Task<IEnumerable<FactoryWithCarbonFootprintDto>> GetFactoriesExceedingCarbonThresholdAsync(float threshold)
        {
            var factoryRepo = unitOfWork.GetRepository<FactoryUser, string>();
            var factories = await factoryRepo.GetAllWithIncludesAsync(f => true, f => f.ApplicationUser!, f => f.FactoryEmissions!);

            var exceedingFactories = factories
                .Select(factory => new
                {
                    Factory = factory,
                    TotalEmission = factory.FactoryEmissions!.OrderByDescending(f => f.Date).FirstOrDefault()?.CarbonEmission ?? 0
                })
                .Where(x => x.TotalEmission > threshold)
                .Select(x => new FactoryWithCarbonFootprintDto
                {
                    FactoryId = x.Factory.Id,
                    FactoryName = x.Factory.ApplicationUser?.DisplayName ?? "Unknown",
                    TotalCarbonEmission = x.TotalEmission
                });

            return exceedingFactories;
        }

        public async Task<string> CreateReportOfAllCalculationsAsync(string factoryId, string agentId) 
        {
            var factoryRepo = unitOfWork.GetRepository<FactoryUser, string>();
            var emissionRepo = unitOfWork.GetRepository<FactoryEmission, string>();
            var reportRepo = unitOfWork.GetRepository<Report, string>();

            var agent = await unitOfWork.GetRepository<EnvironmentalAgent, string>().GetByConditionAsync(a => a.ApplicationUserId == agentId);
            if (agent == null)
                throw new Exception("Agent not found");

            var factory = await factoryRepo.GetByConditionWithIncludesAsync(
                f => f.Id == factoryId,
                f => f.ApplicationUser!
            );

            if (factory == null)
                throw new Exception("Factory not found");

            var emissions = await emissionRepo.GetAllByConditionAsync(e => e.FactoryUserId == factoryId);

            if (!emissions.Any())
                throw new Exception("No emissions found for this factory");

            // Build a report body with all calculations
            var reportBody = new StringBuilder();
            reportBody.AppendLine($"Report for Factory: {factory.ApplicationUser?.DisplayName}");
            reportBody.AppendLine($"Email: {factory.ApplicationUser?.Email}");
            reportBody.AppendLine($"Industry Type: {factory.IndustryType}");
            reportBody.AppendLine($"Generated On: {DateTime.UtcNow}");
            reportBody.AppendLine("\n-------- All Carbon Footprint Records --------");

            foreach (var e in emissions.OrderByDescending(e => e.Date))
            {
                reportBody.AppendLine($"""
                        
                        ----------------------------------
                        Emission Date: {e.Date}
                        Carbon Emission: {e.CarbonEmission} kg CO₂
                        Electricity: {e.ElectricityConsumptionAmount} ({e.ElectricityConsumptionType})
                        Fuel: {e.FuelConsumptionAmount} ({e.FuelConsumptionType})
                        Waste: {e.WasteGenerated} ({e.WasteType})
                        Water: {e.WaterConsumption}
                    """);
            }

            var report = new Report
            {
                Id = Guid.NewGuid().ToString(),
                FactoryUserId = factoryId,
                EnvironmentalAgentId = agent.Id,
                ReportBody = reportBody.ToString(),
                DateGenerated = DateTime.UtcNow,
                OnLatestCalculation = false
            };

            await reportRepo.AddAsync(report);
            await unitOfWork.SaveChangesAsync();

            return "All-calculations report created successfully.";
        }

        public async Task<string> CreateReportOfLatestCalculationAsync(string factoryId, string agentId)
        {
            var factoryRepo = unitOfWork.GetRepository<FactoryUser, string>();
            var emissionRepo = unitOfWork.GetRepository<FactoryEmission, string>();
            var reportRepo = unitOfWork.GetRepository<Report, string>();

            var agent = await unitOfWork.GetRepository<EnvironmentalAgent, string>().GetByConditionAsync(a => a.ApplicationUserId == agentId);
            if (agent == null)
                throw new Exception("Agent not found");

            // Get factory with user info
            var factory = await factoryRepo.GetByConditionWithIncludesAsync(
                f => f.Id == factoryId,
                f => f.ApplicationUser!
            );

            if (factory == null)
                throw new Exception("Factory not found");

            // Get latest emission entry
            var allSortedEmissions = await emissionRepo
                .GetAllByConditionSortedAsync(e => e.FactoryUserId == factoryId, f => f.Date , ascending: false);

            var latestEmission = allSortedEmissions.FirstOrDefault();

            if (latestEmission == null)
                throw new Exception("No emission data found for the factory");

            // Create report content in a simple readable way
            var reportBody = $"""
                     Report for Factory: {factory.ApplicationUser?.DisplayName}
                     Email: {factory.ApplicationUser?.Email}
                     Industry Type: {factory.IndustryType}

                     Generated On: {DateTime.UtcNow}

                     --- Carbon Emission Summary ---
                     Carbon Emission: {latestEmission.CarbonEmission} kg CO₂
                     Electricity Consumption: {latestEmission.ElectricityConsumptionAmount}       ({latestEmission.ElectricityConsumptionType})
                     Fuel Consumption: {latestEmission.FuelConsumptionAmount} ({latestEmission.FuelConsumptionType})
                     Water Consumption: {latestEmission.WaterConsumption}
                     Waste Generated: {latestEmission.WasteGenerated} ({latestEmission.WasteType})

                     Emission Date: {latestEmission.Date}
                 """;

            var report = new Report
            {
                Id = Guid.NewGuid().ToString(),
                FactoryUserId = factoryId,
                EnvironmentalAgentId = agent.Id,
                ReportBody = reportBody,
                DateGenerated = DateTime.UtcNow,
                OnLatestCalculation = true
            };

            await reportRepo.AddAsync(report);
            await unitOfWork.SaveChangesAsync();

            return "Report created successfully.";
        }

        public async Task<IEnumerable<ReportDto>> GetReportsByCalculationTypeAsync(string agentId, bool onLatestCalculation)
        {
            var agent = await unitOfWork.GetRepository<EnvironmentalAgent, string>().GetByConditionAsync(a => a.ApplicationUserId == agentId);
            if (agent == null)
                throw new Exception("Agent not found");

            var reportRepo = unitOfWork.GetRepository<Report, string>();
            var reports = await reportRepo.GetAllWithIncludesAsync(
                r => r.EnvironmentalAgentId == agent.Id &&
                     r.OnLatestCalculation == onLatestCalculation,
                r => r.Factory!,
                r => r.Factory!.ApplicationUser!,
                r => r.EnvironmentalAgent!,
                r => r.EnvironmentalAgent!.ApplicationUser!
            );

            return reports.Select(r => new ReportDto
            {
                Id = r.Id,
                OnLatestCalculation = r.OnLatestCalculation,
                DateGenerated = r.DateGenerated,
                FactoryName = r.Factory!.ApplicationUser!.DisplayName,
                FactoryEmail = r.Factory.ApplicationUser.Email!,
                AgentName = r.EnvironmentalAgent!.ApplicationUser!.DisplayName,
                AgentEmail = r.EnvironmentalAgent.ApplicationUser.Email!,
                ReportBody = r.ReportBody
            });
        }

        public async Task<IEnumerable<ReportDto>> GetAllReportsOfFactoryAsync(string agentId, string factoryId, bool onLatestCalculation)
        {
            var agent = await unitOfWork.GetRepository<EnvironmentalAgent, string>().GetByConditionAsync(a => a.ApplicationUserId == agentId);
            if (agent == null)
                throw new Exception("Agent not found");

            var reportRepo = unitOfWork.GetRepository<Report, string>();

            var reports = await reportRepo.GetAllWithIncludesAsync(
                r => r.FactoryUserId == factoryId && 
                     r.EnvironmentalAgentId == agent.Id &&
                     r.OnLatestCalculation == onLatestCalculation,
                r => r.Factory!,
                r => r.Factory!.ApplicationUser!,
                r => r.EnvironmentalAgent!,
                r => r.EnvironmentalAgent!.ApplicationUser!
            );

            return reports.Select(r => new ReportDto
            {
                Id = r.Id,
                OnLatestCalculation = r.OnLatestCalculation,
                DateGenerated = r.DateGenerated,
                FactoryName = r.Factory!.ApplicationUser!.DisplayName,
                FactoryEmail = r.Factory.ApplicationUser.Email!,
                AgentName = r.EnvironmentalAgent!.ApplicationUser!.DisplayName,
                AgentEmail = r.EnvironmentalAgent.ApplicationUser.Email!,
                ReportBody = r.ReportBody
            });
        }

        public async Task<ReportDto> GetReportByIdAsync(string reportId)
        {
            var reportRepo = unitOfWork.GetRepository<Report, string>();
            var report = await reportRepo.GetByConditionWithIncludesAsync(
                r => r.Id == reportId,
                r => r.Factory!.ApplicationUser!,
                r => r.EnvironmentalAgent!.ApplicationUser!
            );

            if (report == null)
                throw new Exception("Report not found");

            return new ReportDto
            {
                Id = report.Id,
                OnLatestCalculation = report.OnLatestCalculation,
                DateGenerated = report.DateGenerated,
                FactoryName = report.Factory?.ApplicationUser?.DisplayName ?? "Unknown Factory",
                FactoryEmail = report.Factory?.ApplicationUser?.Email ?? "",
                AgentName = report.EnvironmentalAgent?.ApplicationUser?.DisplayName ?? "Unknown Agent",
                AgentEmail = report.EnvironmentalAgent?.ApplicationUser?.Email ?? "",
                ReportBody = report.ReportBody
            };
        }

        public async Task SendReportToAgentAsync(string reportId)
        {
            var reportRepo = unitOfWork.GetRepository<Report, string>();

            var report = await reportRepo.GetByConditionWithIncludesAsync(
                r => r.Id == reportId,
                r => r.EnvironmentalAgent!.ApplicationUser!,
                r => r.Factory!.ApplicationUser!
            );

            if (report == null)
                throw new Exception("Report not found");

            var agentEmail = report.EnvironmentalAgent?.ApplicationUser?.Email;
            if (string.IsNullOrWhiteSpace(agentEmail))
                throw new Exception("Agent email not found");

            var subject = $"Environmental Report - {report.DateGenerated:dd/MM/yyyy}";

            var body = $@"
                <h2>Environmental Report</h2>
                <p><strong>Factory:</strong> {report.Factory?.ApplicationUser?.DisplayName}</p>
                <p><strong>Email:</strong> {report.Factory?.ApplicationUser?.Email}</p>
                <p><strong>Industry Type:</strong> {report.Factory?.IndustryType}</p>
                <p><strong>Generated On:</strong> {report.DateGenerated:dd/MM/yyyy HH:mm:ss}</p>
                <hr />
                <h3>Report Body</h3>
                <p>{report.ReportBody.Replace(Environment.NewLine, "<br/>")}</p>
            ";

            await emailService.SendEmailAsync(agentEmail, subject, body);
        }
        public async Task DeleteReportAsync(string reportId)
        {
            var reportRepo = unitOfWork.GetRepository<Report, string>();

            var report = await reportRepo.GetAsync(reportId);
            if (report == null)
                throw new Exception("Report not found");

            reportRepo.Delete(report);
            await unitOfWork.SaveChangesAsync();
        }

        /* This method is commented out because it requires a PDF generation service.
         * Uncomment and implement the PDF generation logic when ready.
         
        public async Task SendReportToAgentAsPdfAsync(string reportId)
        {
            var reportRepo = unitOfWork.GetRepository<Report, string>();

            var report = await reportRepo.GetByConditionWithIncludesAsync(
                r => r.Id == reportId,
                r => r.EnvironmentalAgent!.ApplicationUser!,
                r => r.Factory!.ApplicationUser!
            );

            if (report == null)
                throw new Exception("Report not found");

            var agentEmail = report.EnvironmentalAgent?.ApplicationUser?.Email;
            if (string.IsNullOrEmpty(agentEmail))
                throw new Exception("Agent email not found");

            var subject = $"Environmental Report - {report.DateGenerated:dd/MM/yyyy}";

            var htmlContent = $@"
                <h2>Environmental Report</h2>
                <p><strong>Factory:</strong> {report.Factory?.ApplicationUser?.DisplayName}</p>
                <p><strong>Email:</strong> {report.Factory?.ApplicationUser?.Email}</p>
                <p><strong>Industry Type:</strong> {report.Factory?.IndustryType}</p>
                <p><strong>Generated On:</strong> {report.DateGenerated:dd/MM/yyyy HH:mm:ss}</p>
                <hr />
                <h3>Report Body</h3>
                <p>{report.ReportBody.Replace(Environment.NewLine, "<br/>")}</p>
    
            ";

            var pdfBytes = pdfService.GenerateReportPdf(htmlContent);

            await emailService.SendEmailWithAttachmentAsync(agentEmail, subject, "Please find the report attached.", pdfBytes, "EnvironmentalReport.pdf");

        }

        */


    }
}
