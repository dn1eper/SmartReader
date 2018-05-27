
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
	book_id INT PRIMARY KEY,
	remark TEXT,
	offset INT UNSIGNED NOT NULL,
	FOREIGN KEY (book_id)
	REFERENCES book(book_id) ON DELETE CASCADE
);
