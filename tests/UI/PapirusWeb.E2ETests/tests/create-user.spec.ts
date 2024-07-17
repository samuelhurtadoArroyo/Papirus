import { test } from "../Fixtures/login";
import { CreateUserPage } from "../pages/create-user-page";
import { LoginPage } from "../pages/login-page";
import { ProcessesPage } from "../pages/processes-page";
import { UsersPage } from "../pages/users-page";

const initializePages = (webApp) => {
  return {
    processesPage: new ProcessesPage(webApp),
    usersPage: new UsersPage(webApp),
    loginPage: new LoginPage(webApp),
    createUserPage: new CreateUserPage(webApp),
  };
};

test("22 - Verify Navigation to Create User Page", async ({ webApp }) => {
  const { usersPage, processesPage, createUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickAddUserBtn();
  await createUserPage.assertCreateUserPage();
});

test("23 - Verify Return to previous Page", async ({ webApp }) => {
  const { usersPage, processesPage, createUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickAddUserBtn();
  await createUserPage.clickReturnBtn();
});

test("24 - Submit Add User form", async ({ webApp }) => {
  const { usersPage, processesPage, createUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickAddUserBtn();
  await createUserPage.submitCreateUserForm(
    process.env.PAPIRUS_USER || "",
    process.env.PAPIRUS_USER_EMAIL || "",
  );
});

test("25 - Verify Cancel Navigation", async ({ webApp }) => {
  const { usersPage, processesPage, createUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickAddUserBtn();
  await createUserPage.clickCancel();
});

test("26 - Failed Add User Action", async ({ webApp }) => {
  const { usersPage, processesPage, createUserPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickAddUserBtn();
  await createUserPage.clickSave();
});
