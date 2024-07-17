import { expect, Locator, Page } from "@playwright/test";

export class UsersPage {
  page: Page;
  title: Locator;
  addUser: Locator;
  switchStatus: Locator;
  editUser: Locator;
  search: Locator;

  constructor(page: Page) {
    this.page = page;
    this.title = page.locator("#title-h1");
    this.addUser = page.locator("#add-btn");
    this.switchStatus = page.locator("#user-switch-1");
    this.editUser = page.locator("#user-edit-1-a");
    this.search = page.locator("#search");
  }

  async assertUsersPage() {
    await expect(this.title).toBeVisible();
    await expect(this.addUser).toBeVisible();
  }

  async clickAddUserBtn() {
    await this.addUser.click();
  }

  async checkSwitchStatus() {
    await this.switchStatus.check();
  }

  async uncheckSwitchStatus() {
    await this.switchStatus.uncheck();
  }

  async clickEditUser() {
    await this.editUser.click();
  }

  async searchUser() {
    await this.search.fill(process.env.PAPIRUS_USER || "");
    await expect(
      this.page.getByText(process.env.PAPIRUS_USER || ""),
    ).toBeVisible();
  }
}
