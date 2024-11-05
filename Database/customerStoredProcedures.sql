DELIMITER // 
CREATE PROCEDURE usp_CustomerDelete (in custID int, in conCurrId int)
BEGIN
	Delete from customers where customerID = custID and ConcurrencyID = conCurrId;
END //
DELIMITER ; 

DELIMITER // 
CREATE PROCEDURE usp_CustomerCreate (in custID int, in name varchar(100), in address varchar(50), in city varchar(20), in state char(2), in zipCode char(15))
BEGIN
	Insert into customers (CustomerID, Name, Address, City, State, ZipCode) values (custID, name, address, city, state, zipCode);
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerSelect (in custID int)
BEGIN
	Select * from customers where customerID = custID;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerSelectAll ()
BEGIN
	Select * from customers order by Name;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerUpdate (in custID int, in name varchar(100), in conCurrId int)
BEGIN
	Update customers
    Set Name = name, concurrencyid = (concurrencyid + 1)
    Where customerID = custID and concurrencyid = conCurrId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerCreate (out custId int, in name_p varchar(100), in address_p varchar(50), in city_p varchar(20), in state_p varchar(2), in zipcode_p varchar(15))
BEGIN
	Insert into customers (name, address, city, state, zipcode, concurrencyid)
    Values (name_p, address_p, city_p, state_p, zipcode_p, 1);
    Select LAST_INSERT_ID() into custId;
    
END //
DELIMITER ; 