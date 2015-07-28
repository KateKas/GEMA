-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Pessoas'
CREATE TABLE [dbo].[Pessoas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(250)  NOT NULL
);
GO

-- Creating table 'Comentarios'
CREATE TABLE [dbo].[Comentarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
	Titulo varchar(100)  NOT NULL,
    [Comentario] varchar(max)  NOT NULL,
    [DataComentario] datetime  NOT NULL,
    [Pessoas_Id] int  NOT NULL
);
GO

-- Creating table 'Secoes'
CREATE TABLE [dbo].[Secoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descricao] varchar(500)  NOT NULL,
    [Secao] varchar(50)  NOT NULL
);
GO

-- Creating table 'Materias'
CREATE TABLE [dbo].[Materias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Titulo] varchar(100)  NOT NULL,
    [Descricao] varchar(500)  NOT NULL,
    [DataMateria] datetime  NOT NULL,
    [Materia] varchar(max)  NOT NULL,
    [Condicao] varchar(20)  NOT NULL,
    [Secoes_Id] int  NOT NULL,
    [Revisores_Id] int  NULL,
    [Jornalistas_Id] int  NOT NULL,
    [Gerentes_Id] int  NOT NULL,
    [Publicadores_Id] int  NULL
);
GO

-- Creating table 'Eventos'
CREATE TABLE [dbo].[Eventos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Evento] varchar(100)  NOT NULL,
    [DataEvento] Datetime  NOT NULL,
    [Pessoas_Id] int  NOT NULL,
    [Materias_Id] int  NOT NULL
);
GO

-- Creating table 'Pessoas_Revisores'
CREATE TABLE [dbo].[Pessoas_Revisores] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Pessoas_Jornalistas'
CREATE TABLE [dbo].[Pessoas_Jornalistas] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Pessoas_Gerentes'
CREATE TABLE [dbo].[Pessoas_Gerentes] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Pessoas_Publicadores'
CREATE TABLE [dbo].[Pessoas_Publicadores] (
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------


-- Creating primary key on [Id] in table 'Pessoas_Revisores'
ALTER TABLE [dbo].[Pessoas_Revisores]
ADD CONSTRAINT [PK_Pessoas_Revisores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pessoas_Jornalistas'
ALTER TABLE [dbo].[Pessoas_Jornalistas]
ADD CONSTRAINT [PK_Pessoas_Jornalistas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pessoas_Gerentes'
ALTER TABLE [dbo].[Pessoas_Gerentes]
ADD CONSTRAINT [PK_Pessoas_Gerentes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pessoas_Publicadores'
ALTER TABLE [dbo].[Pessoas_Publicadores]
ADD CONSTRAINT [PK_Pessoas_Publicadores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Pessoas_Id] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [FK_PessoasComentarios]
    FOREIGN KEY ([Pessoas_Id])
    REFERENCES [dbo].[Pessoas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PessoasComentarios'
CREATE INDEX [IX_FK_PessoasComentarios]
ON [dbo].[Comentarios]
    ([Pessoas_Id]);
GO

-- Creating foreign key on [Pessoas_Id] in table 'Eventos'
ALTER TABLE [dbo].[Eventos]
ADD CONSTRAINT [FK_PessoasEventos]
    FOREIGN KEY ([Pessoas_Id])
    REFERENCES [dbo].[Pessoas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PessoasEventos'
CREATE INDEX [IX_FK_PessoasEventos]
ON [dbo].[Eventos]
    ([Pessoas_Id]);
GO

-- Creating foreign key on [Secoes_Id] in table 'Materias'
ALTER TABLE [dbo].[Materias]
ADD CONSTRAINT [FK_SecoesMaterias]
    FOREIGN KEY ([Secoes_Id])
    REFERENCES [dbo].[Secoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SecoesMaterias'
CREATE INDEX [IX_FK_SecoesMaterias]
ON [dbo].[Materias]
    ([Secoes_Id]);
GO

-- Creating foreign key on [Materias_Id] in table 'Eventos'
ALTER TABLE [dbo].[Eventos]
ADD CONSTRAINT [FK_MateriasEventos]
    FOREIGN KEY ([Materias_Id])
    REFERENCES [dbo].[Materias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MateriasEventos'
CREATE INDEX [IX_FK_MateriasEventos]
ON [dbo].[Eventos]
    ([Materias_Id]);
GO

-- Creating foreign key on [Revisores_Id] in table 'Materias'
ALTER TABLE [dbo].[Materias]
ADD CONSTRAINT [FK_RevisoresMaterias]
    FOREIGN KEY ([Revisores_Id])
    REFERENCES [dbo].[Pessoas_Revisores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RevisoresMaterias'
CREATE INDEX [IX_FK_RevisoresMaterias]
ON [dbo].[Materias]
    ([Revisores_Id]);
GO

-- Creating foreign key on [Jornalistas_Id] in table 'Materias'
ALTER TABLE [dbo].[Materias]
ADD CONSTRAINT [FK_JornalistasMaterias]
    FOREIGN KEY ([Jornalistas_Id])
    REFERENCES [dbo].[Pessoas_Jornalistas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JornalistasMaterias'
CREATE INDEX [IX_FK_JornalistasMaterias]
ON [dbo].[Materias]
    ([Jornalistas_Id]);
GO

-- Creating foreign key on [Gerentes_Id] in table 'Materias'
ALTER TABLE [dbo].[Materias]
ADD CONSTRAINT [FK_GerentesMaterias]
    FOREIGN KEY ([Gerentes_Id])
    REFERENCES [dbo].[Pessoas_Gerentes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GerentesMaterias'
CREATE INDEX [IX_FK_GerentesMaterias]
ON [dbo].[Materias]
    ([Gerentes_Id]);
GO

-- Creating foreign key on [Publicadores_Id] in table 'Materias'
ALTER TABLE [dbo].[Materias]
ADD CONSTRAINT [FK_PublicadoresMaterias]
    FOREIGN KEY ([Publicadores_Id])
    REFERENCES [dbo].[Pessoas_Publicadores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PublicadoresMaterias'
CREATE INDEX [IX_FK_PublicadoresMaterias]
ON [dbo].[Materias]
    ([Publicadores_Id]);
GO

-- Creating foreign key on [Id] in table 'Pessoas_Revisores'
ALTER TABLE [dbo].[Pessoas_Revisores]
ADD CONSTRAINT [FK_Revisores_inherits_Pessoas]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Pessoas]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Pessoas_Jornalistas'
ALTER TABLE [dbo].[Pessoas_Jornalistas]
ADD CONSTRAINT [FK_Jornalistas_inherits_Pessoas]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Pessoas]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Pessoas_Gerentes'
ALTER TABLE [dbo].[Pessoas_Gerentes]
ADD CONSTRAINT [FK_Gerentes_inherits_Pessoas]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Pessoas]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Pessoas_Publicadores'
ALTER TABLE [dbo].[Pessoas_Publicadores]
ADD CONSTRAINT [FK_Publicadores_inherits_Pessoas]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Pessoas]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------