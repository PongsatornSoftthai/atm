ALTER TABLE TmMasterData
ADD CONSTRAINT FK_TmMasterData_TmDataType
FOREIGN KEY (nTypeID)
REFERENCES TmDataType (nTypeID);