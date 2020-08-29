using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain;
using Expenses.Domain.KeylessModels;
using Expenses.Infrastructure.Application.Enums;
using Expenses.Infrastructure.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Application.Queries.Handlers
{
    public class GetBudgetReportQueryHandler : IRequestHandler<GetBudgetReportQuery, IEnumerable<BudgetReport>>
    {
        private readonly ExpensesDbContext _dbContext;

        public GetBudgetReportQueryHandler(ExpensesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<BudgetReport>> Handle(GetBudgetReportQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<BudgetReport> report;
            //todo January = 0 from API, this could be done in front-end or back-end
            request.Month++;
            switch (request.ReportType) 
            {
                case BudgetReportEnum.CategoryReport :
                    report = await GetCategoryBudgetReport(request.Month, request.Year, request.UserId);
                    // report.ReportType = BudgetReportEnum.CategoryReport;
                    break;
                case BudgetReportEnum.AccountReport :
                    report = await GetAccountBudgetReport(request.Month, request.Year, request.UserId);
                    // report.ReportType = BudgetReportEnum.AccountReport;
                    break;
                default: report = null;
                    break;
            }

            return report;
        }

        private async Task<IEnumerable<BudgetReport>> GetCategoryBudgetReport(int month, int year, string userId)
        {
            var result = _dbContext.BudgetReports.FromSqlRaw("SELECT * FROM dbo.budget_category_report r " +
                                                             "WHERE r.month = {0} and r.year = {1} and r.user_id = {2}",
                month, year, userId);

            return result;
        }
        
        private async Task<IEnumerable<BudgetReport>> GetAccountBudgetReport(int month, int year, string userId)
        {
            var result = _dbContext.BudgetReports.FromSqlRaw("SELECT * FROM dbo.budget_account_report r " +
                                                             "WHERE r.month = {0} and r.year = {1} and r.user_id = {2}",
                month, year, userId);

            return result;
        }
    }
}