import { IExtractedData } from "@/domain/interfaces/IExtractedData";

export const extractedData = {
  autoadmit: [
    { id: "dueTerms", label: "Termino de vencimiento", value: "1 día" },
    { id: "courtEmail", label: "Correo Juzgado / Notifica", value: "" },
    { id: "courtName", label: "Juzgado", value: "JUZGADO DIECISIETE CIVIL MUNICIPAL DE BUCARAMANGA" },
    { id: "filed", label: "Radicado", value: "68001400301720240019900" },
    { id: "claimerName", label: "Accionante", value: "MARIA DILCE CUADROS" },
    { id: "defendantName", label: "Accionado", value: "BANCOLOMBIA S.A" },
  ],
  email: [
    { id: "claimerName", label: "Accionante", value: "MARIA DILCE CUADROS" },
    {
      id: "courtEmail",
      label: "Correo Juzgado / Notifica",
      value: "j17cmbuc@cendoj.ramajudicial.gov.co",
    },
    {
      id: "claimerNameIdNumber",
      label: "Nro. de identificación del accionante",
      value: "CC 63319301",
    },
    {
      id: "regionalBanking",
      label: "Regional bancario",
      value: "TUTELASCENTRO",
    },
    { id: "state", label: "Departamento", value: "Santander" },
    { id: "city", label: "Ciudad", value: "Bucaramanga" },
    {
      id: "assignDate",
      label: "Fecha Asignación Bancolombia",
      value: "Viernes, 8 de marzo de 2024, 14:50:00 PM",
    },
    {
      id: "courtNotificationDate",
      label: "Fecha Notificación Juzgado",
      value: "Viernes, 8 de marzo de 2024, 11:35:00 PM",
    },
    {
      id: "responseDeadline",
      label: "Fecha Límite Respuesta",
      value: "Lunes, 11 de marzo de 2024, 05:00:00 PM",
    },
  ],
} as IExtractedData;
