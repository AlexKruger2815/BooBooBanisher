Alter table users drop constraint unique_username;
ALTER TABLE users ADD CONSTRAINT unique_username UNIQUE (username);
ALTER TABLE sessions ADD CONSTRAINT check_date CHECK (createdat <= CURRENT_TIMESTAMP);
