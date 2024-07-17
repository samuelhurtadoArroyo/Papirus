import { test } from "../Fixtures/login";
import { ProcessesPage } from "../pages/processes-page";
import { LoginPage } from "../pages/login-page";
const initializePages = (webApp) => {
  return {
    processesPage: new ProcessesPage(webApp),
    loginPage: new LoginPage(webApp),
  };
};

test("10 - Verify Navigation to Processes Page", async ({ webApp }) => {
  const { processesPage } = initializePages(webApp);
  await processesPage.assertProcessesPage();
});

test("11 - Verify Navigation to Guardianships Page", async ({ webApp }) => {
  const { processesPage } = initializePages(webApp);
  await processesPage.clickGuardianshipsBtn();
});
