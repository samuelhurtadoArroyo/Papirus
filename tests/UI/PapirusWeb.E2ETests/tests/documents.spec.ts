import { test } from "../Fixtures/login";
import { LoginPage } from "../pages/login-page";
import { ProcessesPage } from "../pages/processes-page";
import { DocumentsPage } from "../pages/documents-page";
import { GuardianshipsPage } from "../pages/guardianships-page";

const initializePages = (webApp) => {
  return {
    processesPage: new ProcessesPage(webApp),
    documentsPage: new DocumentsPage(webApp),
    guardianshipsPage: new GuardianshipsPage(webApp),
    loginPage: new LoginPage(webApp),
  };
};

test("31 - Verify Navigation to Documents Page", async ({ webApp }) => {
  const { documentsPage, processesPage, guardianshipsPage } =
    initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.clickRedirectToGuardianship();
  await documentsPage.assertDocumentsPage();
});

test("32 - Verify Navigation to Document Page", async ({ webApp }) => {
  const { documentsPage, processesPage, guardianshipsPage } =
    initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.clickRedirectToGuardianship();
  await documentsPage.clickRedirectToDocument();
  await documentsPage.assertDocumentPage();
});

test("34 - Verify Continue Action", async ({ webApp }) => {
  const { documentsPage, processesPage, guardianshipsPage } =
    initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.clickRedirectToGuardianship();
  await documentsPage.clickContinue();
});

test("35 - Verify Download Action", async ({ webApp }) => {
  const { documentsPage, processesPage, guardianshipsPage } =
    initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.clickRedirectToGuardianship();
  await documentsPage.clickDownload();
});
