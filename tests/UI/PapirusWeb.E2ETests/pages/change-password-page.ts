import { expect, Locator, Page } from "@playwright/test";

export class ChangePasswordPage {
  page: Page;
  title: Locator;
  return: Locator;
  currentPassword: Locator;
  newPassword: Locator;
  confirmPassword: Locator;
  cancel: Locator;
  save: Locator;

  constructor(page: Page) {
    this.page = page;
    this.title = page.locator("#title-h1");
    this.return = page.locator("#return-btn");
    this.currentPassword = page.locator("#currentPassword");
    this.newPassword = page.locator("#newPassword");
    this.confirmPassword = page.locator("#confirmPassword");
    this.cancel = page.locator("#cancel-btn");
    this.save = page.locator("#save-btn");
  }

  async assertChangePasswordPage() {
    await expect(this.title).toBeVisible();
  }

  async clickReturnBtn() {
    await this.return.click();
  }

  async submitChangePasswordForm(currentPassword, newPassword) {
    await this.currentPassword.fill(currentPassword);
    await this.newPassword.fill(newPassword);
    await this.confirmPassword.fill(newPassword);
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

  async assertSuccessToast() {
    await expect(this.page.locator(".p-toast-message-success")).toBeVisible();
  }
}
