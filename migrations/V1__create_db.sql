create table public.Users(
	userID SERIAL PRIMARY KEY,
	username varchar(50) not null
)
;

create table public.MessageCategory(
	MessageCategoryID SERIAL PRIMARY KEY,
	MessageCategoryType varchar(50) not null
)
;

create table public.Messages(
	messageID SERIAL PRIMARY KEY,
	messageCategoryID INT REFERENCES public.MessageCategory(MessageCategoryID),
	messageContent varchar(10000) not null
)
;

create table public.Sessions(
	SessionID SERIAL PRIMARY KEY,
	userID int references public.Users(userID),
	messageID int references public.Messages(messageID),
	createdAt timestamp
)
;