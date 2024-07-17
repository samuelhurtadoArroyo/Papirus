import { test } from "../Fixtures/login";
import { LoginPage } from "../pages/login-page";
import { ProcessesPage } from "../pages/processes-page";
import { UsersPage } from "../pages/users-page";

const initializePages = (webApp) => {
  return {
    processesPage: new ProcessesPage(webApp),
    usersPage: new UsersPage(webApp),
    loginPage: new LoginPage(webApp),
  };
};

test("12 - Verify Navigation to Users Page", async ({ webApp }) => {
  const { usersPage, processesPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.assertUsersPage();
});

test("13 - Verify Navigation to Add User Page", async ({ webApp }) => {
  const { usersPage, processesPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickAddUserBtn();
});

test("14 - Verify User Search", async ({ webApp }) => {
  const { usersPage, processesPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.searchUser();
});

test("15 - Verify Navigation to Edit User Page", async ({ webApp }) => {
  const { usersPage, processesPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.clickEditUser();
});

test("16 - Verify User Status Switch", async ({ webApp }) => {
  const { usersPage, processesPage } = initializePages(webApp);
  await processesPage.clickUsersLink();
  await usersPage.checkSwitchStatus();
});
