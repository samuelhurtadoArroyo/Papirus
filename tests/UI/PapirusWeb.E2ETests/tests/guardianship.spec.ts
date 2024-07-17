import { test } from "../Fixtures/login";
import { LoginPage } from "../pages/login-page";
import { ProcessesPage } from "../pages/processes-page";
import { GuardianshipsPage } from "../pages/guardianships-page";

const initializePages = (webApp) => {
  return {
    processesPage: new ProcessesPage(webApp),
    guardianshipsPage: new GuardianshipsPage(webApp),
    loginPage: new LoginPage(webApp),
  };
};

test("31 - Verify Navigation to Guardianships Page", async ({ webApp }) => {
  const { guardianshipsPage, processesPage } = initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.assertGuardianshipsPage();
});

test("32 - Verify Navigation to Guardianship Page", async ({ webApp }) => {
  const { guardianshipsPage, processesPage } = initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.clickRedirectToGuardianship();
});

test("33 - Verify Search", async ({ webApp }) => {
  const { guardianshipsPage, processesPage } = initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.searchGuardianship();
});

test("34 - Verify Reassign Action", async ({ webApp }) => {
  const { guardianshipsPage, processesPage } = initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.clickReassign();
  await guardianshipsPage.clickSave();
});

test("35 - Verify Cancel Reassign Action", async ({ webApp }) => {
  const { guardianshipsPage, processesPage } = initializePages(webApp);
  await processesPage.clickGuardianshipsLink();
  await guardianshipsPage.clickReassign();
  await guardianshipsPage.clickCancel();
});
