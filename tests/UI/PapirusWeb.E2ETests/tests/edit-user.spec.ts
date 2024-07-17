import { test } from "../Fixtures/login";
import { EditUserPage } from "../pages/edit-user-page";
import { LoginPage } from "../pages/login-page";
import { ProcessesPage } from "../pages/processes-page";
import { UsersPage } from "../pages/users-page";

const initializePages = (webApp) => {
  return {
    processesPage: new ProcessesPage(webApp),
    usersPage: new UsersPage(webApp),
    loginPage: new LoginPage(webApp),
    editUserPage: new EditUserPage(webApp),
  };
};

test("17 - Verify Navigation to Edit User Page", async ({ webApp }) => {
  const { usersPage, processesPage, editUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickEditUser();
  await editUserPage.assertEditUserPage();
});

test("18 - Verify Return to previous Page", async ({ webApp }) => {
  const { usersPage, processesPage, editUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickEditUser();
  await editUserPage.clickReturnBtn();
});

test("19 - Submit Updated User form", async ({ webApp }) => {
  const { usersPage, processesPage, editUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickEditUser();
  await editUserPage.submitEditUserForm(
    process.env.PAPIRUS_USER || "",
    process.env.PAPIRUS_USER_EMAIL || "",
  );
});

test("20 - Verify Cancel Navigation", async ({ webApp }) => {
  const { usersPage, processesPage, editUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickEditUser();
  await editUserPage.clickCancel();
});

test("21 - Failed Update User Action", async ({ webApp }) => {
  const { usersPage, processesPage, editUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickEditUser();
  await editUserPage.clickSave();
  await editUserPage.assertErrorToast();
});
