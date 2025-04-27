CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    user_name character varying(64) NOT NULL,
	password text NOT NULL,
	email character varying(64) NOT NULL,
	created_at TIMESTAMP with time zone NOT NULL,
	suspended BOOLEAN NOT NULL
);

CREATE TABLE access_tokens (
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    Value TEXT NOT NULL,
    created_at TIMESTAMP with time zone NOT NULL,
    expires_at TIMESTAMP with time zone NOT NULL,
    revoked BOOLEAN NOT NULL,
	FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE refresh_tokens (
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    Value TEXT NOT NULL,
    created_at TIMESTAMP with time zone NOT NULL,
    expires_at TIMESTAMP with time zone NOT NULL,
    revoked BOOLEAN NOT NULL,
	FOREIGN KEY (user_id) REFERENCES users(id)
);