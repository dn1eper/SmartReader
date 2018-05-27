
USE smart_reader;
CREATE TABLE book (
	book_id INT AUTO_INCREMENT PRIMARY KEY,
	title VARCHAR(100) NOT NULL,
	content TEXT
);

CREATE TABLE person (
	login VARCHAR(100) PRIMARY KEY,
	pass_hash CHAR(100) NOT NULL
);

CREATE TABLE book_login (
	login VARCHAR(100) PRIMARY KEY,
	book_id INT,
	FOREIGN KEY (login)
	REFERENCES person(login),
	FOREIGN KEY (book_id)
	REFERENCES book(book_id) ON DELETE CASCADE
);

CREATE TABLE bookmark (
	book_id INT PRIMARY KEY,
	remark TEXT,
	offset INT UNSIGNED NOT NULL,
	FOREIGN KEY (book_id)
	REFERENCES book(book_id) ON DELETE CASCADE
);

CREATE TABLE token (
	login VARCHAR(100) PRIMARY KEY,
	token CHAR(100)	UNIQUE,
	FOREIGN KEY (login)
	REFERENCES person(login)
);