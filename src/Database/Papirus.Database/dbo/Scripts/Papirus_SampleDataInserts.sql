-- Roles
INSERT INTO Roles (Name) VALUES 
('Administrador'), 
('Usuario'),
('Super Usuario');

-- Permissions
INSERT INTO Permissions (Name, Description) VALUES 
('Demandas', 'Administración de Demandas'), 
('Tutelas', 'Administración Tutelas'),
('Configuracion', 'Configuración');


-- RolePermissions
INSERT INTO RolePermissions (RoleId, PermissionId) VALUES 
(1, 1), -- Administrador, Demandas
(1, 2), -- Administrador, Tutelas
(1, 3), -- Administrador, Configuración
(2, 1), -- Usuario, Demandas
(2, 2); -- Usuario, Tutelas

-- Firms
INSERT INTO Firms (Name) VALUES 
('Gomez Pineda'), 
('Alianza');

-- Users
INSERT INTO Users (Email, FirstName, LastName, HashedPassword, Salt, RegistrationDate, Active, RoleId, FirmId) VALUES 
('Papirus.Administrator@email.com', 'Papirus', 'Administrador', 'tXJlkMcMxJhHvZ2RK6SQShIrBzWAJjPwkFHQLz23GTY=', 'YzbbWdkVjn3JNFe1l/IJmA==', GETDATE(), 1, 1, 1), -- Password*01
('Basic.User@email.com', 'Basic', 'User', 'TOFVyw0h3sWJMLk2s+gAljU0V2iNbgK2xBPWBX2gPsw=', 'JMHuzQKqE5CXuOTpPkqjDw==', GETDATE(), 1, 2, 1), -- Password*01
('Super.User@email.com', 'Super', 'User', 'pZTwjSj8Iz7tLE/nKcW8v6Fl89YLPOMiLbQ4KmVNlLk=', '/x2XaXyVuu6cAKTsIQJBgQ==', GETDATE(), 1, 3, 1); -- Password*01




-- Insertando datos en ProcessTypes
INSERT INTO ProcessTypes (Name) VALUES ('Tipo de Proceso 1'), ('Tipo de Proceso 2');

-- Insertando datos en Processes
INSERT INTO Processes (Name, ProcessTypeId) VALUES 
('Proceso 1', 1), 
('Proceso 2', 2);



-- Insertando datos en SubProcesses
INSERT INTO SubProcesses (Description, ProcessId) VALUES 
('SubProceso 1 para Proceso 1', 1), 
('SubProceso 2 para Proceso 2', 2);




-- Insertando datos en Demandants
INSERT INTO Demandants (Name) VALUES 
('Demandante 1'), 
('Demandante 2');

-- Insertando datos en Demands
-- Asegúrate de que las fechas y los identificadores únicos sean válidos y únicos.
INSERT INTO Demands (RegistrationDate, DemandIdentifier, Court, City, ProcessTypeId, ProcessId, SubProcessId, DemandantId) VALUES 
(GETDATE(), NEWID(), 'Corte 1', 'Ciudad 1', 1, 1, 1, 1), 
(GETDATE(), NEWID(), 'Corte 2', 'Ciudad 2', 2, 2, 2, 2);

-- Insertando datos en Defendants
-- Asume DemandId válido basado en Demands insertados anteriormente.
INSERT INTO Defendants (FirstName, LastName, Email, IdentificationNumber, DemandId, SupportFile) VALUES 
('Defensor 1', 'Apellido1', 'defensor1@example.com', '12345678', 1, 0x123456), 
('Defensor 2', 'Apellido2', 'defensor2@example.com', '87654321', 2, 0x123456);

-- Insertando datos en DocumentTypes
INSERT INTO DocumentTypes (Name, FormatTypeId, ExtractionStrategyId, Readable, ClassExtractor) VALUES 
('TipoDoc 1', 1, 1, 1, 'Extractor 1'), 
('TipoDoc 2', 2, 2, 1, 'Extractor 2');

-- Insertando datos en FormatTypes
INSERT INTO FormatTypes (Name, Extensions) VALUES 
('PDF', '.pdf'), 
('Word', '.docx');

-- Insertando datos en ExtractionStrategies
INSERT INTO ExtractionStrategies (Name) VALUES 
('Estrategia 1'), 
('Estrategia 2');

-- Users
