DROP PROCEDURE if EXISTS usp_ProductCreate;
DROP PROCEDURE if EXISTS usp_ProductSelect;
DROP PROCEDURE if EXISTS usp_ProductSelectAll;
DROP PROCEDURE if EXISTS usp_ProductUpdate;
DROP PROCEDURE if EXISTS usp_ProductDelete;
DROP PROCEDURE if EXISTS usp_SaveOrUpdateProduct;

DELIMITER // 
CREATE PROCEDURE usp_ProductDelete (in prodID int, in conCurrId int)
BEGIN
	Delete from products where ProductID = prodID and ConcurrencyID = conCurrId;
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
    Set ProductCode = prodCode, ConcurrencyID = (ConcurrencyID + 1)
    Where ProductID = prodID and ConcurrencyID = conCurrId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductCreate (out prodID int, in prodCode varchar(10), in prodDesc varchar(50), in prodUnitPrice decimal(10,4), in prodOnHandQuantity int)
BEGIN
	Insert into products (ProductCode, Description, UnitPrice, OnHandQuantity, concurrencyid)
    Values (prodCode, prodDesc, prodUnitPrice, prodOnHandQuantity, 1);
    Select LAST_INSERT_ID() into prodID;
    
END //
DELIMITER ; 

DELIMITER //
CREATE PROCEDURE usp_SaveOrUpdateProduct (out prodID int, in prodCode varchar(10), in description varchar(50), in unitPrice decimal(10,4), in onHandQuantity int)
BEGIN
	DECLARE existingProductID int DEFAULT NULL;
    SELECT id INTO existngProductID FROM products
    WHERE ProductCode = prodCode
    LIMIT 1;
    
    IF existingProductID IS NOT NULL THEN
		UPDATE Products
        SET Description = description,
			UnitPrice = unitPrice,
            OnHandQuantity = onHandQuantity,
            ConcurrencyID = ConcurrencyID + 1
		WHERE id = existingProductID;
        
        SET prodID = existingProductID;
	ELSE 
		INSERT INTO Products (ProductCode, Description, UnitPrice, OnHandQuantity, ConcurrencyID)
        VALUES (prodCode, description, unitPrice, onHandQuantity, 1);
        
        SET prodID = last_insert_id();
	END IF;
END //
DELIMITER ;