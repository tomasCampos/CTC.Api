namespace CTC.Application.Features.CostCenter
{
    internal static class CostCenterSqlScripts
    {
        #region INSERT

        public static string INSERT_COST_CENTER = @"INSERT INTO `heroku_3a06699194dd49a`.`cost_center`
                                                    (`cost_center_id`,
                                                    `cost_center_name`,
                                                    `cost_center_observations`,
                                                    `cost_center_starting_date`,
                                                    `cost_center_closing_forecast_date`,
                                                    `cost_center_closing_date`,
                                                    `address_id`,
                                                    `client_id`)
                                                    VALUES
                                                    (@cost_center_id,
                                                    @cost_center_name,
                                                    @cost_center_observations,
                                                    @cost_center_starting_date,
                                                    @cost_center_closing_forecast_date,
                                                    @cost_center_closing_date,
                                                    @address_id,
                                                    @client_id);";

        public static string INSERT_COST_CENTER_ADDRESS = @"INSERT INTO `heroku_3a06699194dd49a`.`address`
                                                            (`address_id`,
                                                            `address_postal_code`,
                                                            `address_street`,
                                                            `address_neighborhood`,
                                                            `address_number`,
                                                            `address_complement`,
                                                            `address_city`,
                                                            `address_state`)
                                                            VALUES
                                                            (@address_id,
                                                            @address_postal_code,
                                                            @address_street,
                                                            @address_neighborhood,
                                                            @address_number,
                                                            @address_complement,
                                                            @address_city,
                                                            @address_state);";

        #endregion

        #region

        public static string COUNT_COST_CENTER_BY_NAME = "select count(*) from cost_center where cost_center_name = @cost_center_name";
        public static string COUNT_CLIENT_BY_ID = "select count(*) `heroku_3a06699194dd49a`.client c where c.client_id = @client_id";

        #endregion
    }
}
