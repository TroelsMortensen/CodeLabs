DROP SCHEMA IF EXISTS webshop CASCADE;
CREATE SCHEMA webshop;
SET SCHEMA 'webshop';

CREATE TABLE customer
(
    customer_id   INTEGER      NOT NULL,
    gender        VARCHAR(1),
    first_name    VARCHAR(255) NOT NULL,
    middle_name   VARCHAR(255),
    last_name     VARCHAR(255) NOT NULL,
    birthdate     DATE         NOT NULL,
    email_address VARCHAR(255) NOT NULL,
    phone_number  VARCHAR(20)  NOT NULL,
    password      VARCHAR(60)  NOT NULL,
    PRIMARY KEY (customer_id),
    UNIQUE (email_address)
);

CREATE TABLE "order"
(
    order_id            INTEGER      NOT NULL,
    customer_id         INTEGER      NOT NULL,
    email_address       VARCHAR(255) NOT NULL,
    purchase_date       DATE,
    billing_address_id  INTEGER,
    delivery_address_id INTEGER,
    PRIMARY KEY (order_id)
);

CREATE TABLE orderedproduct
(
    orderedproduct_id   INTEGER     NOT NULL,
    product_id          INTEGER     NOT NULL,
    quantity            INTEGER     NOT NULL,
    order_id            INTEGER     NOT NULL,
    PRIMARY KEY (orderedproduct_id)
);


CREATE TABLE manufacturer
(
    manufacturer_id   INTEGER     NOT NULL,
    manufacturer_name VARCHAR(32) NOT NULL,
    PRIMARY KEY (manufacturer_id)
);

CREATE TABLE product
(
    product_id             INTEGER     NOT NULL,
    product_name           VARCHAR(64) NOT NULL,
    product_description    TEXT,
    product_price          DECIMAL(7,2)       NOT NULL,
    product_url            VARCHAR(255),
    product_date_available DATE,
    product_change_date    DATE,
    manufacturer_id        INTEGER,
    PRIMARY KEY (product_id)
);

CREATE TABLE review
(
    review_id     INTEGER NOT NULL,
    review_rating INTEGER NOT NULL,
    review_reads  INTEGER NOT NULL,
    review_text   TEXT,
    reviewer      INTEGER,
    product_id    INTEGER,
    PRIMARY KEY (review_id)
);

CREATE TABLE basket
(
    basket_id         INTEGER NOT NULL,
    customer_id       INTEGER NOT NULL,
    basket_date_added DATE,
    PRIMARY KEY (basket_id)
);

CREATE TABLE basketproducts
(
    basket_id  INTEGER NOT NULL,
    product_id INTEGER NOT NULL,
    quantity   INT,
    PRIMARY KEY (basket_id, product_id)
);

CREATE TABLE address
(
    address_id     INTEGER NOT NULL,
    address_number  INTEGER,
    address_street VARCHAR(80),
    zip_code        VARCHAR(10),
    customer_id    INTEGER,
    PRIMARY KEY (address_id)
);

CREATE TABLE city
(
    city_id   INTEGER NOT NULL,
    city_name VARCHAR(80),
    PRIMARY KEY (city_id)
);

CREATE TABLE stateorprovince
(
    stateorprovince_id  INTEGER NOT NULL,
    stateorprovincename VARCHAR(80),
    PRIMARY KEY (stateorprovince_id)
);

CREATE TABLE country
(
    country_code VARCHAR(2) NOT NULL,
    country_name VARCHAR(80),
    PRIMARY KEY (country_code)
);

CREATE TABLE zipcode
(
    zip_code           VARCHAR(10) NOT NULL,
    city_id            INTEGER,
    country_code       VARCHAR(2),
    stateorprovince_id INTEGER,
    PRIMARY KEY (zip_code)
);

ALTER TABLE address
    ADD FOREIGN KEY (zip_code)
        REFERENCES zipcode (zip_code)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (customer_id)
        REFERENCES customer (customer_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE;

ALTER TABLE basket
    ADD FOREIGN KEY (customer_id)
        REFERENCES customer (customer_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE;

ALTER TABLE basketproducts
    ADD FOREIGN KEY (basket_id)
        REFERENCES basket (basket_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (product_id)
        REFERENCES product (product_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE;

ALTER TABLE "order"
    ADD FOREIGN KEY (billing_address_id)
        REFERENCES address (address_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (delivery_address_id)
        REFERENCES address (address_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (customer_id)
        REFERENCES customer (customer_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (email_address)
        REFERENCES customer (email_address)
        ON DELETE SET NULL
        ON UPDATE CASCADE;

ALTER TABLE orderedproduct
    ADD FOREIGN KEY (order_id)
        REFERENCES "order" (order_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (product_id)
        REFERENCES product (product_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE;

ALTER TABLE product
    ADD FOREIGN KEY (manufacturer_id)
        REFERENCES manufacturer(manufacturer_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE;

ALTER TABLE review
    ADD FOREIGN KEY (reviewer)
        REFERENCES customer (customer_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (product_id)
        REFERENCES product (product_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE;

ALTER TABLE zipcode
    ADD FOREIGN KEY (city_id)
        REFERENCES city (city_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (country_code)
        REFERENCES country (country_code)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    ADD FOREIGN KEY (stateorprovince_id)
        REFERENCES stateorprovince (stateorprovince_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE;