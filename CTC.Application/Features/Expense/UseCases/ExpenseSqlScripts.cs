namespace CTC.Application.Features.Expense.UseCases
{
    internal static class ExpenseSqlScripts
    {
        #region SELECT

        

        #endregion

        #region INSERT

        public static string INSERT_EXPENSE = @"INSERT INTO `heroku_3a06699194dd49a`.`expense`
                                                (`expense_id`,
                                                `supplier_id`,
                                                `transaction_id`)
                                                VALUES
                                                (@expense_id,
                                                @supplier_id,
                                                @transaction_id);";

        #endregion
    }
}
