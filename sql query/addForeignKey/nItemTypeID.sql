ALTER TABLE TbAtmDataHistory
ADD CONSTRAINT FK_TbAtmDataHistory_TmMasterData
FOREIGN KEY (nItemTypeID)
REFERENCES TmMasterData (nMasterID);