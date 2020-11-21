-- Pessoa definition
CREATE TABLE Pessoa (
  [id] int NOT NULL IDENTITY,
  [nome] varchar(255) NOT NULL,
  [cpf] varchar(30) NOT NULL,
  PRIMARY KEY ([id]),
);

-- Endereco definition
CREATE TABLE Endereco (
  [id] int NOT NULL IDENTITY,
  [logradouro] varchar(255) NOT NULL,
  [numero] int DEFAULT NULL,
  [cep] varchar(8) DEFAULT NULL,
  [bairro] varchar(255) DEFAULT NULL,
  [cidade] varchar(255) DEFAULT NULL,
  [estado] varchar(2) DEFAULT NULL,
  [pessoa_ref] int NOT NULL,
  PRIMARY KEY ([id]),
  CONSTRAINT [Endereco_FK_1] FOREIGN KEY ([pessoa_ref]) REFERENCES Pessoa ([id])
);

CREATE INDEX [Endereco_estado_IDX] ON Endereco ([estado]);
CREATE INDEX [Endereco_FK_1] ON Endereco ([pessoa_ref]);

-- TipoTelefone definition
CREATE TABLE TipoTelefone (
  [id] int NOT NULL IDENTITY,
  [tipo] varchar(100) DEFAULT NULL,
  PRIMARY KEY ([id])
);

-- Telefone definition
CREATE TABLE Telefone (
  [id] int NOT NULL IDENTITY,
  [numero] varchar(30) NOT NULL,
  [ddd] int DEFAULT NULL,
  [tipo] int DEFAULT NULL,
  [id_pessoa] int NOT NULL,
  PRIMARY KEY ([id]),
  CONSTRAINT [Telefone_FK] FOREIGN KEY ([tipo]) REFERENCES TipoTelefone ([id]),
  CONSTRAINT [Telefone_FK_1] FOREIGN KEY ([id_pessoa]) REFERENCES Pessoa ([id])
);

CREATE INDEX [Telefone_ddd_IDX] ON Telefone ([ddd]);
CREATE INDEX [Telefone_FK] ON Telefone ([tipo]);
CREATE INDEX [Telefone_FK_1] ON Telefone ([id_pessoa]);
