ALTER TABLE TbAdmin
ADD CONSTRAINT FK_TbAdmin_TmMasterData
FOREIGN KEY (nTitleID)
REFERENCES TmMasterData (nMasterID);