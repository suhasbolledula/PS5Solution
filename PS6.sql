CREATE TABLE ATTR 
(
  ATTR_ID VARCHAR2(32) NOT NULL 
, ATTR_NAME VARCHAR2(200) NOT NULL 
, ATTR_DATATYPE VARCHAR2(20) NOT NULL 
, ATTR_LENGTH NUMBER(9) NOT NULL 
, ATTR_PRECISION NUMBER(9) 
, ATTR_CRTD_ID VARCHAR2(40) NOT NULL 
, ATTR_CRTD_DT DATE NOT NULL 
, ATTR_UPDT_ID VARCHAR2(40) NOT NULL 
, ATTR_UPDT_DT DATE NOT NULL 
, CONSTRAINT ATTR_PK PRIMARY KEY 
  (
    ATTR_ID 
  )
  ENABLE 
);

CREATE TABLE ATTR_VAL 
(
  ATTR_VAL_ID VARCHAR2(32) NOT NULL 
, ATTR_VAL_VAL VARCHAR2(4000) NOT NULL 
, ATTR_VAL_CRTD_ID VARCHAR2(40) NOT NULL 
, ATTR_VAL_CRTD_DT DATE NOT NULL 
, ATTR_VAL_UPDT_ID VARCHAR2(40) NOT NULL 
, ATTR_VAL_UPDT_DT DATE NOT NULL 
, CONSTRAINT ATTR_VAL_PK PRIMARY KEY 
  (
    ATTR_VAL_ID 
  )
  ENABLE 
);

CREATE TABLE PRODUCT_ATTR 
(
  PRODUCT_ATTR_ID VARCHAR2(32) NOT NULL 
, PRODUCT_ATTR_PRODUCT_ID VARCHAR2(32) NOT NULL 
, PRODUCT_ATTR_REQ_IND NUMBER(1) NOT NULL 
, PRODUCT_ATTR_ATTR_ID VARCHAR2(32) NOT NULL 
, PRODUCT_ATTR_CRTD_ID VARCHAR2(40) NOT NULL 
, PRODUCT_ATTR_CRTD_DT DATE NOT NULL 
, PRODUCT_ATTR_UPDT_ID VARCHAR2(40) NOT NULL 
, PRODUCT_ATTR_UPDT_DT DATE NOT NULL 
, CONSTRAINT PRODUCT_ATTR_PK PRIMARY KEY 
  (
    PRODUCT_ATTR_ID 
  )
  ENABLE 
);

CREATE TABLE INVENTORY_ATTR_VAL 
(
  INVENTORY_ATTR_VAL_ID VARCHAR2(32) NOT NULL 
, INVENTORY_ATTR_VAL_INVENTORY_ID VARCHAR2(32) NOT NULL 
, INVENTORY_ATTR_VAL_PRODUCT_ATTR_ID VARCHAR2(32) NOT NULL 
, INVENTORY_ATTR_VAL_ATTR_VAL_ID VARCHAR2(32) NOT NULL 
, INVENTORY_ATTR_VAL_CRTD_ID VARCHAR2(40) NOT NULL 
, INVENTORY_ATTR_VAL_CRTD_DT DATE NOT NULL 
, INVENTORY_ATTR_VAL_UPDT_ID VARCHAR2(40) NOT NULL 
, INVENTORY_ATTR_VAL_UPDT_DT DATE NOT NULL 
, CONSTRAINT INVENTORY_ATTR_VAL_PK PRIMARY KEY 
  (
    INVENTORY_ATTR_VAL_ID 
  )
  ENABLE 
);

ALTER TABLE PRODUCT_ATTR
ADD CONSTRAINT PRODUCT_ATTR_FK1 FOREIGN KEY
(
  PRODUCT_ATTR_PRODUCT_ID 
)
REFERENCES PRODUCT
(
  PRODUCT_ID 
)
ENABLE;

ALTER TABLE PRODUCT_ATTR
ADD CONSTRAINT PRODUCT_ATTR_FK2 FOREIGN KEY
(
  PRODUCT_ATTR_ATTR_ID 
)
REFERENCES ATTR
(
  ATTR_ID 
)
ENABLE;

ALTER TABLE INVENTORY 
DROP COLUMN INVENTORY_SERIAL_NBR;

ALTER TABLE INVENTORY_ATTR_VAL
ADD CONSTRAINT INVENTORY_ATTR_VAL_FK1 FOREIGN KEY
(
  INVENTORY_ATTR_VAL_INVENTORY_ID 
)
REFERENCES INVENTORY
(
  INVENTORY_ID 
)
ENABLE;

ALTER TABLE INVENTORY_ATTR_VAL
ADD CONSTRAINT INVENTORY_ATTR_VAL_FK2 FOREIGN KEY
(
  INVENTORY_ATTR_VAL_PRODUCT_ATTR_ID 
)
REFERENCES PRODUCT_ATTR
(
  PRODUCT_ATTR_ID 
)
ENABLE;

ALTER TABLE INVENTORY_ATTR_VAL
ADD CONSTRAINT INVENTORY_ATTR_VAL_FK3 FOREIGN KEY
(
  INVENTORY_ATTR_VAL_ATTR_VAL_ID 
)
REFERENCES ATTR_VAL
(
  ATTR_VAL_ID 
)
ENABLE;

CREATE OR REPLACE TRIGGER trg01_attr BEFORE
    INSERT OR UPDATE ON attr
    FOR EACH ROW
BEGIN
    IF inserting THEN
        :new.attr_crtd_id := user;
        :new.attr_crtd_dt := sysdate;
    END IF;

    :new.attr_updt_id := user;
    :new.attr_updt_dt := sysdate;
END;
/

CREATE OR REPLACE TRIGGER trg01_attr_val BEFORE
    INSERT OR UPDATE ON attr_val
    FOR EACH ROW
BEGIN
    IF inserting THEN
        :new.attr_val_crtd_id := user;
        :new.attr_val_crtd_dt := sysdate;
    END IF;

    :new.attr_val_updt_id := user;
    :new.attr_val_updt_dt := sysdate;
END;
/

CREATE OR REPLACE TRIGGER trg01_product_attr BEFORE
    INSERT OR UPDATE ON product_attr
    FOR EACH ROW
BEGIN
    IF inserting THEN
        :new.product_attr_crtd_id := user;
        :new.product_attr_crtd_dt := sysdate;
    END IF;

    :new.product_attr_updt_id := user;
    :new.product_attr_updt_dt := sysdate;
END;
/

CREATE OR REPLACE TRIGGER trg01_inventory_attr_val BEFORE
    INSERT OR UPDATE ON inventory_attr_val
    FOR EACH ROW
BEGIN
    IF inserting THEN
        :new.inventory_attr_val_crtd_id := user;
        :new.inventory_attr_val_crtd_dt := sysdate;
    END IF;

    :new.inventory_attr_val_updt_id := user;
    :new.inventory_attr_val_updt_dt := sysdate;
END;
/

CREATE OR REPLACE TRIGGER trg01_product BEFORE
    INSERT OR UPDATE ON product
    FOR EACH ROW
BEGIN
    IF inserting THEN
        :new.product_crtd_id := user;
        :new.product_crtd_dt := sysdate;
    END IF;

    :new.product_updt_id := user;
    :new.product_updt_dt := sysdate;
END;
/

CREATE OR REPLACE TRIGGER trg01_product_status BEFORE
    INSERT OR UPDATE ON product_status
    FOR EACH ROW
BEGIN
    IF inserting THEN
        :new.product_status_crtd_id := user;
        :new.product_status_crtd_dt := sysdate;
    END IF;

    :new.product_status_updt_id := user;
    :new.product_status_updt_dt := sysdate;
END;
/

CREATE OR REPLACE TRIGGER trg01_inventory BEFORE
    INSERT OR UPDATE ON inventory
    FOR EACH ROW
BEGIN
    IF inserting THEN
        :new.inventory_crtd_id := user;
        :new.inventory_crtd_dt := sysdate;
    END IF;

    :new.inventory_updt_id := user;
    :new.inventory_updt_dt := sysdate;
END;
/

CREATE OR REPLACE TRIGGER trg02_attr BEFORE
    INSERT ON attr
    FOR EACH ROW
BEGIN
    IF :new.attr_id IS NULL THEN
        :new.attr_id := sys_guid();
    END IF;
END;
/

CREATE OR REPLACE TRIGGER trg02_attr_val BEFORE
    INSERT ON attr_val
    FOR EACH ROW
BEGIN
    IF :new.attr_val_id IS NULL THEN
        :new.attr_val_id := sys_guid();
    END IF;
END;
/

CREATE OR REPLACE TRIGGER trg02_product_attr BEFORE
    INSERT ON product_attr
    FOR EACH ROW
BEGIN
    IF :new.product_attr_id IS NULL THEN
        :new.product_attr_id := sys_guid();
    END IF;
END;
/

CREATE OR REPLACE TRIGGER trg02_inventory_attr_val BEFORE
    INSERT ON inventory_attr_val
    FOR EACH ROW
BEGIN
    IF :new.inventory_attr_val_id IS NULL THEN
        :new.inventory_attr_val_id := sys_guid();
    END IF;
END;
/

CREATE OR REPLACE TRIGGER trg02_product BEFORE
    INSERT ON product
    FOR EACH ROW
BEGIN
    IF :new.product_id IS NULL THEN
        :new.product_id := sys_guid();
    END IF;
END;
/

CREATE OR REPLACE TRIGGER trg02_product_status BEFORE
    INSERT ON product_status
    FOR EACH ROW
BEGIN
    IF :new.product_status_id IS NULL THEN
        :new.product_status_id := sys_guid();
    END IF;
END;
/

CREATE OR REPLACE TRIGGER trg02_inventory BEFORE
    INSERT ON inventory
    FOR EACH ROW
BEGIN
    IF :new.inventory_id IS NULL THEN
        :new.inventory_id := sys_guid();
    END IF;
END;
/


-- Insert products
INSERT INTO Product (Product_ID, Name) VALUES 
(1, 'iPhone 15'),
(2, 'Samsung Galaxy S22'),
(3, 'Google Pixel 6');
/

-- Insert attributes
INSERT INTO Attr (Attr_ID, Name) VALUES 
(1, 'Weight'),
(2, 'Color'),
(3, 'Battery Life'),
(4, 'Storage Capacity'),
(5, 'Screen Size');

-- Link products and attributes (Assuming all attributes are required)
-- iPhone 15
INSERT INTO Product_Attr (Product_ID, Attr_ID, Product_Attr_Req_Ind) VALUES 
(1, 1, 1),
(1, 2, 1),
(1, 3, 1),
(1, 4, 1),
(1, 5, 1);

-- Samsung Galaxy S22
INSERT INTO Product_Attr (Product_ID, Attr_ID, Product_Attr_Req_Ind) VALUES 
(2, 1, 1),
(2, 2, 1),
(2, 3, 1),
(2, 4, 1),
(2, 5, 1);

-- Google Pixel 6
INSERT INTO Product_Attr (Product_ID, Attr_ID, Product_Attr_Req_Ind) VALUES 
(3, 1, 1),
(3, 2, 1),
(3, 3, 1),
(3, 4, 1),
(3, 5, 1);

-- Add inventory items
-- iPhone 15
INSERT INTO Inventory (Inventory_ID, Product_ID) VALUES 
(1, 1), (2, 1), (3, 1), (4, 1), (5, 1), (6, 1), (7, 1);

-- Samsung Galaxy S22
INSERT INTO Inventory (Inventory_ID, Product_ID) VALUES 
(8, 2), (9, 2), (10, 2), (11, 2), (12, 2), (13, 2), (14, 2);

-- Google Pixel 6
INSERT INTO Inventory (Inventory_ID, Product_ID) VALUES 
(15, 3), (16, 3), (17, 3), (18, 3), (19, 3), (20, 3);

-- Add attribute values for each inventory item
-- iPhone 15, Inventory_ID 1 to 7
INSERT INTO Inventory_Attr_Val (Inventory_ID, Attr_ID, Value) VALUES 
(1, 1, '174 grams'), (1, 2, 'Black'), (1, 3, '18 hours'), (1, 4, '128 GB'), (1, 5, '6.1 inches'),
(2, 1, '174 grams'), (2, 2, 'Black'), (2, 3, '18 hours'), (2, 4, '128 GB'), (2, 5, '6.1 inches'),
(3, 1, '174 grams'), (3, 2, 'Black'), (3, 3, '18 hours'), (3, 4, '128 GB'), (3, 5, '6.1 inches'),
(4, 1, '174 grams'), (4, 2, 'Black'), (4, 3, '18 hours'), (4, 4, '128 GB'), (4, 5, '6.1 inches'),
(5, 1, '174 grams'), (5, 2, 'Black'), (5, 3, '18 hours'), (5, 4, '128 GB'), (5, 5, '6.1 inches'),
(6, 1, '174 grams'), (6, 2, 'Black'), (6, 3, '18 hours'), (6, 4, '128 GB'), (6, 5, '6.1 inches'),
(7, 1, '174 grams'), (7, 2, 'Black'), (7, 3, '18 hours'), (7, 4, '128 GB'), (7, 5, '6.1 inches');

-- Samsung Galaxy S22, Inventory_ID 8 to 14
INSERT INTO Inventory_Attr_Val (Inventory_ID, Attr_ID, Value) VALUES 
(8, 1, '167 grams'), (8, 2, 'White'), (8, 3, '20 hours'), (8, 4, '256 GB'), (8, 5, '6.2 inches'),
(9, 1, '167 grams'), (9, 2, 'White'), (9, 3, '20 hours'), (9, 4, '256 GB'), (9, 5, '6.2 inches'),
(10, 1, '167 grams'), (10, 2, 'White'), (10, 3, '20 hours'), (10, 4, '256 GB'), (10, 5, '6.2 inches'),
(11, 1, '167 grams'), (11, 2, 'White'), (11, 3, '20 hours'), (11, 4, '256 GB'), (11, 5, '6.2 inches'),
(12, 1, '167 grams'), (12, 2, 'White'), (12, 3, '20 hours'), (12, 4, '256 GB'), (12, 5, '6.2 inches'),
(13, 1, '167 grams'), (13, 2, 'White'), (13, 3, '20 hours'), (13, 4, '256 GB'), (13, 5, '6.2 inches'),
(14, 1, '167 grams'), (14, 2, 'White'), (14, 3, '20 hours'), (14, 4, '256 GB'), (14, 5, '6.2 inches');

-- Google Pixel 6, Inventory_ID 15 to 20
INSERT INTO Inventory_Attr_Val (Inventory_ID, Attr_ID, Value) VALUES 
(15, 1, '207 grams'), (15, 2, 'Sorta Seafoam'), (15, 3, '24 hours'), (15, 4, '128 GB'), (15, 5, '6.4 inches'),
(16, 1, '207 grams'), (16, 2, 'Sorta Seafoam'), (16, 3, '24 hours'), (16, 4, '128 GB'), (16, 5, '6.4 inches'),
(17, 1, '207 grams'), (17, 2, 'Sorta Seafoam'), (17, 3, '24 hours'), (17, 4, '128 GB'), (17, 5, '6.4 inches'),
(18, 1, '207 grams'), (18, 2, 'Sorta Seafoam'), (18, 3, '24 hours'), (18, 4, '128 GB'), (18, 5, '6.4 inches'),
(19, 1, '207 grams'), (19, 2, 'Sorta Seafoam'), (19, 3, '24 hours'), (19, 4, '128 GB'), (19, 5, '6.4 inches'),
(20, 1, '207 grams'), (20, 2, 'Sorta Seafoam'), (20, 3, '24 hours'), (20, 4, '128 GB'), (20, 5, '6.4 inches');
