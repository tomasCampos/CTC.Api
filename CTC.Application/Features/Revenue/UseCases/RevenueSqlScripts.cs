namespace CTC.Application.Features.Revenue.UseCases
{
    internal static class RevenueSqlScripts
    {
        #region INSERT

        public static string INSERT_REVENUE = @"INSERT INTO `heroku_3a06699194dd49a`.`revenue`
                                                (`revenue_id`,
                                                `client_id`,
                                                `transaction_id`)
                                                VALUES
                                                (@revenue_id,
                                                @client_id,
                                                @transaction_id);";

        #endregion
    }
}
