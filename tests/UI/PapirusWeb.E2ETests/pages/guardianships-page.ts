import { expect, Locator, Page } from "@playwright/test";

export class GuardianshipsPage {
  page: Page;
  title: Locator;
  search: Locator;
  redirectToGuardianship: Locator;
  reassign: Locator;
  save: Locator;
  cancel: Locator;

  constructor(page: Page) {
    this.page = page;
    this.title = page.locator("#title-h1");
    this.search = page.locator("#search");
    this.redirectToGuardianship = page.locator("#redirect-to-guardianship-1-a");
    this.reassign = page.locator("#reassign-guardianship-1-btn");
    this.save = page.locator("#save-reassign-1-btn");
    this.cancel = page.locator("#cancel-reassign-1-btn");
  }

  async assertGuardianshipsPage() {
    await expect(this.title).toBeVisible();
    await expect(this.search).toBeVisible();
  }

  async clickRedirectToGuardianship() {
    await this.redirectToGuardianship.click();
  }

  async clickReassign() {
    await this.reassign.click();
  }

  async clickSave() {
    await this.save.click();
  }

  async clickCancel() {
    await this.cancel.click();
  }

  async searchGuardianship() {
    await this.search.fill(process.env.PAPIRUS_USER || "");
  }

  async assertErrorToast() {
    await expect(this.page.locator(".p-toast-message-error")).toBeVisible();
  }
}
