ALTER TABLE TbTransactionHistory
ADD CONSTRAINT FK_TbTransactionHistory_TmMasterData
FOREIGN KEY (nItemType)
REFERENCES TmMasterData (nTypeID);