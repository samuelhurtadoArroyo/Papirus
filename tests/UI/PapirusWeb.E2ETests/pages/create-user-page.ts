import { expect, Locator, Page } from "@playwright/test";

export class CreateUserPage {
  page: Page;
  title: Locator;
  return: Locator;
  firstName: Locator;
  lastName: Locator;
  email: Locator;
  password: Locator;
  roles: Locator;
  roleFirstOption: Locator;
  cancel: Locator;
  save: Locator;

  constructor(page: Page) {
    this.page = page;
    this.title = page.locator("#title-h1");
    this.return = page.locator("#return-btn");
    this.firstName = page.locator("#firstName");
    this.lastName = page.locator("#lastName");
    this.email = page.locator("#email");
    this.password = page.locator("#password");
    this.roles = page.locator("#roles-dropdown");
    this.roleFirstOption = page.locator("#option-1");
    this.cancel = page.locator("#cancel-btn");
    this.save = page.locator("#save-btn");
  }

  async assertCreateUserPage() {
    await expect(this.title).toBeVisible();
    await expect(this.return).toBeVisible();
  }

  async clickReturnBtn() {
    await this.return.click();
  }

  async submitCreateUserForm(user, password) {
    await this.firstName.fill(user);
    await this.lastName.fill(user);
    await this.email.fill(user);
    await this.password.fill(password);
    await this.roles.click();
    await this.roleFirstOption.click();
    await this.save.click();
  }

  async clickSave() {
    await this.save.click();
  }

  async clickCancel() {
    await this.cancel.click();
  }

  async assertErrorToast() {
    await expect(this.page.locator(".p-toast-message-error")).toBeVisible();
  }
}
