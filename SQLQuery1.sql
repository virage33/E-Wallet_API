create table dbo.Transactions
(
        TransactionId uniqueidentifier primary key not null,
        TransactionType nchar(50) not null, 
        TransactionAddress nchar(50),
        Remark nchar(50) not null,
        TransactionDate datetime, 
        TransactionAmount money,
        WalletId uniqueidentifier not null,
        constraint Fk_Transaction_Wallet_WalletId foreign key (WalletId) references dbo.Wallet(WalletId),
)