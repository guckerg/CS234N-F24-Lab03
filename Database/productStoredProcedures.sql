DELIMITER // 
CREATE PROCEDURE usp_ProductDelete (in prodID int, in conCurrId int)
BEGIN
	Delete from products where ProductID = prodID and ConcurrencyID = conCurrId;
END //
DELIMITER ; 

DELIMITER // 
CREATE PROCEDURE usp_ProductCreate (in prodID int, in prodCode char(10), in description varchar(50), in unitPrice decimal(10,4), in onHandQuantity int)
BEGIN
	Insert into products (ProductID, ProductCode, Description, UnitPrice, OnHandQuantity) values (prodID, prodCode, description, unitPrice, onHandQuantity);
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductSelect (in prodID int)
BEGIN
	Select * from products where ProductID = prodID;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductSelectAll ()
BEGIN
	Select * from products order by ProductCode;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductUpdate (in prodID int, in prodCode char(10), in conCurrId int)
BEGIN
	Update products
    Set ProductCode = prodCode, concurrencyid = (concurrencyid + 1)
    Where ProductID = prodID and concurrencyid = conCurrId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductCreate (out prodID int, in prodCode char(10), in description varchar(50), in unitPrice int, in onHandQuantity int)
BEGIN
	Insert into products (ProductCode, Description, UnitPrice, OnHandQuantity, concurrencyid)
    Values (prodCode, description, unitPrice, onHandQuantity, 1);
    Select LAST_INSERT_ID() into prodID;
    
END //
DELIMITER ; 