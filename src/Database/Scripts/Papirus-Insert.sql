/* Insert Data */
-- Firms
INSERT INTO [dbo].[Firms] ([GuidIdentifier],[Name]) VALUES 
('85976EF7-D222-446C-AD00-628B9FF47973','Gomez Pineda'), 
('5C9BB7FB-5505-4ED7-B907-872ACA2CECB6','Alianza');

-- Roles
INSERT INTO [dbo].[Roles]([Name]) VALUES 
('Administrador'), 
('Usuario'),
('Super Usuario');

-- Permissions
INSERT INTO [dbo].[Permissions] ([Name],[Description]) VALUES 
('Demandas', 'Administración de Demandas'), 
('Tutelas', 'Administración Tutelas'),
('Configuracion', 'Configuración');

-- RolePermissions
INSERT INTO [dbo].[RolePermissions] ([RoleId],[PermissionId]) VALUES 
(1, 1), -- Administrador, Demandas
(1, 2), -- Administrador, Tutelas
(1, 3), -- Administrador, Configuración
(2, 1), -- Usuario, Demandas
(2, 2), -- Usuario, Tutelas
(3, 3); -- Super Usuario, Configuración

-- Users
INSERT INTO [dbo].[Users] ([Email],[FirstName],[LastName],[HashedPassword],[Salt],[RegistrationDate], [Active], [RoleId], [FirmId]) VALUES 
('Papirus.Administrator@email.com', 'Papirus', 'Administrador', 'tXJlkMcMxJhHvZ2RK6SQShIrBzWAJjPwkFHQLz23GTY=', 'YzbbWdkVjn3JNFe1l/IJmA==', GETDATE(), 1, 1, 1), -- Password*01
('Basic.User@email.com', 'Basic', 'User', 'TOFVyw0h3sWJMLk2s+gAljU0V2iNbgK2xBPWBX2gPsw=', 'JMHuzQKqE5CXuOTpPkqjDw==', GETDATE(), 1, 2, 1), -- Password*01
('Super.User@email.com', 'Super', 'User', 'pZTwjSj8Iz7tLE/nKcW8v6Fl89YLPOMiLbQ4KmVNlLk=', '/x2XaXyVuu6cAKTsIQJBgQ==', GETDATE(), 1, 3, 1); -- Password*01

-- Status
INSERT INTO [dbo].[Status]([Name]) VALUES 
('Pendiente'),
('Asignado'),
('En Proceso'),
('Terminado'),
('Cerrado');

-- ProcessTypes
INSERT INTO [dbo].[ProcessTypes] ([Name]) VALUES  
('Demandas'), 
('Tutelas');

-- Processes
INSERT INTO [dbo].[Processes] ([Name],[ProcessTypeId]) VALUES
('Ejecutivo singular (Personal)', 1),
('Ejecutivo hipotecario', 1),
('Pago directo', 1),
('Abreviado de restitución', 1),
('Reposición de titulos', 1),
('Ejecutivo prendario', 1),
('Ejecutivo mixto', 1),
('Activas', 2),
('Pasivas', 2);

-- SubProcesses
INSERT INTO [dbo].[SubProcesses] ([Description],[Abbreviation],[ProcessId]) VALUES
('Pagare sin abonos, 1 demandado con medida Deceval', 'PSAD-MD', 1),
('Pagare con abonos, 1 demandado y medidas previas', 'PCAD-MP', 1),
('Pagare sin abono, 1 pagare con abono, 1 demandado y con medida Deceval', 'PSAPCAD-MD', 1),
('Dos pagares sin abonos, varios demandados con medida previa', '2PSAVD-MP', 1),
('Varios pagares, 1 demandado con medida Deceval', 'VPAD-MD', 1),
('Derecho de Peticion', null, 9),
('Salud', null, 9),
('Habeas Data', null, 9);

-- PersonTypes
INSERT INTO [dbo].[PersonTypes] ([Name]) VALUES  
('Persona Natural'), 
('Persona Juridica');

-- IdentificationTypes
INSERT INTO [dbo].[IdentificationTypes] ([Name],[Abbreviation]) VALUES  
('Cedula de Ciudadania', 'CC'),
('Cedula de Extranjeria', 'CE'),
('Pasaporte', 'PT'),
('Numero de Identificacion Tributaria', 'NIT'),
('Registro Unico Tributario', 'RUT');

-- ActorTypes
INSERT INTO [dbo].[ActorTypes] ([Name]) VALUES  
('Reclamante'), 
('Defendido');

-- DocumentTypes
INSERT INTO [dbo].[DocumentTypes] ([Name]) VALUES  
('Certificado de Tradicion'),
('Escritura Publica'),
('Pagare'),
('Liquidacion de Credito-F46'),
('Endoso'),
('Liquidacion'),
('Certicamara'),
('Super Intendencia'),
('RepLegalAlianza'),
('RepLegalAbogado'),
('PoderEspecial'),
('SoporteVinculacion'),
('Camara Comercio Bancolombia'),
('Camara Comercio Alianza'),
('Registro Nacional de Abogados(SIRNA)'),
('Certificado Dependientes'),
('Carta de Instrucciones Requerida'),
('Carta de Instrucciones Adicional'),
('Carta de demanda');
