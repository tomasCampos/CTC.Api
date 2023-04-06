namespace CTC.Application.Features.CostCenter.UseCases
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

        #region SELECT

        public static string COUNT_COST_CENTER_BY_NAME = "select count(*) from cost_center where cost_center_name = @cost_center_name";

        public static string COUNT_CLIENT_BY_ID = "select count(*) from `heroku_3a06699194dd49a`.client c where c.client_id = @client_id";

        public static string SELECT_COST_CENTER_BY_ID = @"SELECT 
	                                                        c.cost_center_id AS Id,
                                                            c.cost_center_starting_date AS StartingDate,
                                                            c.cost_center_name AS `Name`,
                                                            c.cost_center_observations AS Observations,
                                                            c.cost_center_closing_forecast_date AS ExpectedClosingDate,
                                                            c.cost_center_closing_date AS ClosingDate,
                                                            c.client_id AS ClientId,
                                                            a.address_city AS AddressCity,
                                                            a.address_complement AS AddressComplement,
                                                            a.address_neighborhood AS AddressNeighborhood,
                                                            a.address_number AS AddressNumber,
                                                            a.address_postal_code AS AddressPostalCode,
                                                            a.address_state AS AddressState,
                                                            a.address_street AS AddressStreetName,
                                                            a.address_id AS AddressId,
                                                            p.person_first_name AS ClientName
                                                        FROM 
	                                                        cost_center c
                                                        INNER JOIN 
	                                                        address a ON c.address_id = a.address_id
                                                        INNER JOIN 
                                                            `heroku_3a06699194dd49a`.client cl ON c.client_id = cl.client_id
                                                        INNER JOIN 
															person p ON cl.person_id = p.person_id
                                                        WHERE 
                                                            c.cost_center_id = @cost_center_id";

        public static string LIST_COST_CENTER_SELECT_STATEMENT = @"SELECT 
	                                                                c.cost_center_id AS Id,
                                                                    c.cost_center_starting_date AS StartingDate,
                                                                    c.cost_center_name AS `Name`,
                                                                    c.cost_center_observations AS Observations,
                                                                    c.cost_center_closing_forecast_date AS ExpectedClosingDate,
                                                                    c.cost_center_closing_date AS ClosingDate,
                                                                    c.client_id AS ClientId,
                                                                    a.address_city AS AddressCity,
                                                                    a.address_complement AS AddressComplement,

                                                                    a.address_neighborhood AS AddressNeighborhood,
                                                                    a.address_number AS AddressNumber,
                                                                    a.address_postal_code AS AddressPostalCode,
                                                                    a.address_state AS AddressState,
                                                                    a.address_street AS AddressStreetName,
                                                                    a.address_id AS AddressId,
                                                                    p.person_first_name AS ClientName";

        public static string LIST_COST_CENTER_FROM_AND_JOIN_STATEMENT = @"FROM 
	                                                                        cost_center c
                                                                        INNER JOIN 
	                                                                        address a ON c.address_id = a.address_id
                                                                        INNER JOIN 
                                                                            `heroku_3a06699194dd49a`.client cl ON c.client_id = cl.client_id
                                                                        INNER JOIN 
															                person p ON cl.person_id = p.person_id";

        public static string LIST_COST_CENTER_WHERE_STATEMENT = @"WHERE 
                                                                  (
                                                                    c.cost_center_name LIKE '%@search_param%'
                                                                    OR p.person_first_name LIKE '%@search_param%' 
                                                                  )";


        #endregion

        #region UPDATE

        public static string UPDATE_COST_CENTER = @"UPDATE 
	                                                    `heroku_3a06699194dd49a`.`cost_center`
                                                    SET
	                                                    `cost_center_name` = @cost_center_name,
	                                                    `cost_center_observations` = @cost_center_observations,
	                                                    `cost_center_starting_date` = @cost_center_starting_date,
	                                                    `cost_center_closing_forecast_date` = @cost_center_closing_forecast_date,
	                                                    `cost_center_closing_date` = @cost_center_closing_date,
	                                                    `client_id` = @client_id
                                                    WHERE 
	                                                    `cost_center_id` = @cost_center_id;";

        public static string UPDATE_COST_CENTER_ADDRESS = @"UPDATE 
	                                                            `heroku_3a06699194dd49a`.`address`
                                                            SET
	                                                            `address_postal_code` = @address_postal_code,
	                                                            `address_street` = @address_street,
	                                                            `address_neighborhood` = @address_neighborhood,
	                                                            `address_number` = @address_number,
	                                                            `address_complement` = @address_complement,
	                                                            `address_city` = @address_city,
	                                                            `address_state` = @address_state
                                                            WHERE 
                                                                `address_id` = @address_id;";

        #endregion

        #region DELETE

        public static string DELETE_COST_CENTER = "DELETE FROM `heroku_3a06699194dd49a`.`cost_center` WHERE cost_center_id = @cost_center_id;";

        public static string DELETE_COST_CENTER_ADDRESS = "DELETE FROM `heroku_3a06699194dd49a`.`address` WHERE `address_id` = @address_id;";

        #endregion
    }
}
