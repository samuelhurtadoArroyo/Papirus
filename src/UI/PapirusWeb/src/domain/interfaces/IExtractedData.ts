import { CaseDocumentFieldValueDto } from "../entities/data-contracts";

export interface IExtractedData {
  autoadmit: IExtractedDataField[];
  email: IExtractedDataField[];
}

export interface IExtractedDataField {
  id: string;
  label: string;
  value: string;
}

export interface IExtractedDataResume {
  documentTypeId: CaseDocumentFieldValueDto["documentTypeId"];
  documentTypeName: CaseDocumentFieldValueDto["documentTypeName"];
  extractedData: CaseDocumentFieldValueDto[];
}
