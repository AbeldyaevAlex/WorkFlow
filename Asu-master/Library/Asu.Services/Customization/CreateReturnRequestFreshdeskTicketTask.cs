namespace Asu.Services.Customization
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Asu.Core.Domain.Returns;
    using Asu.Services.Logging;
    using Asu.Services.Tasks;

    public class CreateReturnRequestFreshdeskTicketTask : ITask
    {
        private const string LockerName = "CreateReturnRequestFreshdeskTicketTask";
        private readonly ICustomService customService;
        private readonly IReturnService returnService;
        private readonly ILogger logger;
        private static readonly Random Randomizer = new Random();

        public CreateReturnRequestFreshdeskTicketTask(ICustomService customService, IReturnService returnService)
        {
            this.customService = customService;
            this.returnService = returnService;
        }

        public void Execute()
        {
            Thread.Sleep(Randomizer.Next(3000, 10000));
            try
            {
                if (!this.customService.SetLockedIfUnlocked(LockerName, 300))
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                this.logger.Error($"Error with CreateReturnRequestFreshdeskTicketTask queue locker checking. {ex.Message}", ex);
                return;
            }

            List<ReturnRequest> returnRequests;
            try
            {
                returnRequests = this.returnService.GetFreshdeskTicketsReturnRequests();
            }
            catch (Exception ex)
            {
                this.logger.Error($"CreateReturnRequestFreshdeskTicketTask error when reading return requests. {ex.Message}", ex);
                this.customService.SetUnlocked(LockerName);
                return;
            }

            returnRequests.ForEach(m => this.returnService.CreateFreshdeskTicket(m));
            this.customService.SetUnlocked(LockerName);
        }
    }
}
