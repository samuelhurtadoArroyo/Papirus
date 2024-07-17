import { test } from "../Fixtures/login";
import { ProcessesPage } from "../pages/processes-page";
import { LoginPage } from "../pages/login-page";
const initializePages = (webApp) => {
  return {
    processesPage: new ProcessesPage(webApp),
    loginPage: new LoginPage(webApp),
  };
};

test("4 - Verify Navigation to Processes Page", async ({ webApp }) => {
  const { processesPage } = initializePages(webApp);
  await processesPage.assertProcessesPage();
});

test("5 - Verify Navigation to Guardianships Page", async ({ webApp }) => {
  const { processesPage } = initializePages(webApp);
  await processesPage.clickGuardianshipsBtn();
});

test("6 - Verify Navigation to Guardianships Page by Menu options", async ({
  webApp,
}) => {
  const { processesPage } = initializePages(webApp);
  await processesPage.assertProcessesPage();
  await processesPage.clickGuardianshipsLink();
});

test("7 - Verify Navigation to Teams Page by Menu options", async ({
  webApp,
}) => {
  const { processesPage } = initializePages(webApp);
  await processesPage.assertProcessesPage();
  await processesPage.clickTeamsLink();
});

test("8 - Verify Navigation to Users Page by Menu options", async ({
  webApp,
}) => {
  const { processesPage } = initializePages(webApp);
  await processesPage.assertProcessesPage();
  await processesPage.clickUsersLink();
});

test("9 - Verify Logout Functionality", async ({ webApp }) => {
  const { processesPage, loginPage } = initializePages(webApp);
  await processesPage.assertProcessesPage();
  await processesPage.clickLogout();
  await loginPage.assertLoginPage();
});
