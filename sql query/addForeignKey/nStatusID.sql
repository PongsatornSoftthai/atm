ALTER TABLE TbTransactionHistory
ADD CONSTRAINT FK_TbTransactionHistory_TmMasterData
FOREIGN KEY (nStatusID)
REFERENCES TmMasterData (nMasterID);