import { expect, Locator, Page } from "@playwright/test";

export class LoginPage {
  page: Page;
  user: Locator;
  password: Locator;
  loginButton: Locator;

  constructor(page: Page) {
    this.page = page;
    this.user = page.locator("#email");
    this.password = page.locator("#password");
    this.loginButton = page.locator("#login-btn");
  }

  async submitLoginForm(user, password) {
    await this.user.fill(user);
    await this.password.fill(password);
    await this.loginButton.click();
  }

  async assertLoginPage() {
    await expect(this.loginButton).toBeVisible();
  }
}
