import { IDocument } from "@/domain/interfaces/IDocument";

export const documents = [
  {
    id: 1,
    fileName: "AutoAdmision",
    documentTypeId: 0,
    processDocumentTypeId: 0,
    caseId: 0,
    filePath: "/tutela-temporal/AutoAdmision.pdf",
  },
  {
    id: 2,
    fileName: "TrasladoTutela",
    documentTypeId: 0,
    processDocumentTypeId: 0,
    caseId: 0,
    filePath: "/tutela-temporal/TrasladoTutela.pdf",
  },
] as IDocument[];

export const responseDocument = {
  id: 3,
  fileName: "CONTESTACION SENCILLA TUTELA",
  documentTypeId: 0,
  processDocumentTypeId: 0,
  caseId: 0,
  filePath: "/tutela-temporal/CONTESTACION SENCILLA TUTELA.docx",
} as IDocument;

export const emergencyBrief = {
  id: 4,
  fileName: "ESCRITO DE EMERGENCIA",
  documentTypeId: 0,
  processDocumentTypeId: 0,
  caseId: 0,
  filePath: "/tutela-temporal/ESCRITO DE EMERGENCIA.docx",
} as IDocument;

export const emailDocument = {
  id: 5,
  fileName: "Email",
  documentTypeId: 0,
  processDocumentTypeId: 0,
  caseId: 0,
  filePath: "/tutela-temporal/email.pdf",
} as IDocument;

export const autoadmitDocument = {
  id: 1,
  fileName: "AutoAdmision",
  documentTypeId: 0,
  processDocumentTypeId: 0,
  caseId: 0,
  filePath: "/tutela-temporal/AutoAdmision.pdf",
} as IDocument;
