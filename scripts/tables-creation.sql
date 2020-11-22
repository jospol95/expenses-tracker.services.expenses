--Run only once or run master-script
--catalog tables
create table account
(
    id          int identity
        primary key,
    name        varchar(30)  not null,
    description varchar(200),
    user_id     varchar(max) not null
)
go

create table category
(
    id              int identity
        primary key,
    name            varchar(30)  not null,
    description     varchar(200),
    user_id         varchar(max) not null,
    budget_assigned decimal(19, 4) default 0
)
--normal tables
go
create table expense
(
    id          varchar(36)    not null
        primary key,
    title       varchar(20)    not null,
    amount      decimal(19, 4) not null,
    date        datetime2      not null,
    description varchar(200),
    user_id     varchar(max)   not null,
    category_id int,
    account_id  int
)
go

create table income
(
    id          varchar(36)    not null
        primary key,
    title       varchar(20)    not null,
    amount      decimal(19, 4) not null,
    date        datetime2      not null,
    description varchar(200),
    user_id     varchar(max)   not null,
    account_id  int
)
go

