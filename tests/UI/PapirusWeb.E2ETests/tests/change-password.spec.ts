import { test } from "../Fixtures/login";
import { ChangePasswordPage } from "../pages/change-password-page";
import { LoginPage } from "../pages/login-page";
import { ProcessesPage } from "../pages/processes-page";

const initializePages = (webApp) => {
  return {
    loginPage: new LoginPage(webApp),
    processesPage: new ProcessesPage(webApp),
    changePasswordPage: new ChangePasswordPage(webApp),
  };
};

test("27 - Verify Navigation to Change Password Page", async ({ webApp }) => {
  const { processesPage, changePasswordPage } = initializePages(webApp);
  await processesPage.clickChangePassword();
  await changePasswordPage.assertChangePasswordPage();
});

test("28 - Submit Updated Password form", async ({ webApp }) => {
  const { processesPage, changePasswordPage } = initializePages(webApp);
  await processesPage.clickChangePassword();
  await changePasswordPage.submitChangePasswordForm(
    process.env.PAPIRUS_USER_EMAIL || "",
    process.env.PAPIRUS_USER_EMAIL || "",
  );
});

test("29 - Verify Cancel Navigation", async ({ webApp }) => {
  const { processesPage, changePasswordPage } = initializePages(webApp);
  await processesPage.clickChangePassword();
  await changePasswordPage.clickCancel();
});

test("30 - Failed Update Password Action", async ({ webApp }) => {
  const { processesPage, changePasswordPage } = initializePages(webApp);
  await processesPage.clickChangePassword();
  await changePasswordPage.clickSave();
});
