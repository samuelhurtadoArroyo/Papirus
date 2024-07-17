export const PERMISSIONS = {
  processes: {
    view: "processes.view",
  },
  config: {
    view: "config.view",
    edit: "config.edit",
  },
  users: {
    view: "users.view",
    edit: "users.edit",
    create: "users.create",
    search: "users.search",
  },
  teams: {
    view: "teams.view",
    assign: "teams.assign",
    create: "teams.create",
    edit: "teams.edit",
    search: "teams.search",
    delete: "teams.delete",
  },
  guardianships: {
    view: "guardianships.view",
    assign: "guardianships.assign",
    download: "guardianships.download",
    answered: "guardianships.answered",
    search: "guardianships.search",
  },
  guardianship: {
    view: "guardianships.view",
    assign: "guardianships.assign",
    download: "guardianships.download",
    answered: "guardianships.answered",
    search: "guardianships.search",
    changeStatus: "guardianships.changeStatus",
  },
  documents: {
    view: "documents.view",
    download: "documents.download",
  },
  document: {
    view: "document.view",
    download: "document.download",
  },
  demands: {
    view: "demands.view",
    search: "demands.search",
  },
  extractedData: {
    view: "extractedData.view",
    edit: "extractedData.edit",
    save: "extractedData.save",
  },
  generateDocument: {
    view: "generateDocument.view",
    emergencyBrief: "generateDocument.emergency",
    responseDocument: "generateDocument.responseDocument",
  },
};