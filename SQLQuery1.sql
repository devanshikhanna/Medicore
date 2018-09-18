SELECT S.*,St.*,P.Name as 'ProductName' FROM Supply AS S 
                                    INNER JOIN Product AS P ON P.ProductID = S.ProductID
                                    INNER JOIN Store AS St ON St.StoreId = S.StoreId
                                    WHERE SalesmanID = 3 and S.Status = 'PENDING'