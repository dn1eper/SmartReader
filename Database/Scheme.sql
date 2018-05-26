-- MariaDB script

USE smart_reader;
CREATE TABLE book (
	book_id INT AUTO_INCREMENT PRIMARY KEY,
	content TEXT
);

CREATE TABLE person (
	login VARCHAR(100) PRIMARY KEY,
	pass_hash CHAR(100) NOT NULL
);

CREATE TABLE bookmark (
	book_id INT,
	remark TEXT,
	offset INT UNSIGNED,
	FOREIGN KEY (book_id)
	REFERENCES book(book_id) ON DELETE CASCADE
);
