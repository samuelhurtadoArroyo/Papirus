import { z } from "zod";
import { emailRegex, passwordRegex } from "./common";
import { textConstants } from "../globalization/es";

const formErrorText = textConstants.components.usersForm.error;

// A Zod schema for the firstName field
const firstNameSchema = z.string().min(2, formErrorText.firstName);

// A Zod schema for the lastName field
const lastNameSchema = z.string().min(2, formErrorText.lastName);

// A Zod schema for the email field
const emailSchema = z.string().regex(emailRegex, formErrorText.email);

// A Zod schema for the password field
const passwordSchema = z.string().regex(passwordRegex, formErrorText.password);

export const CreateUserSchema = z
  .object({
    firstName: firstNameSchema,
    lastName: lastNameSchema,
    email: emailSchema,
    roleId: z.coerce.number().min(1, formErrorText.role),
    password: passwordSchema,
    confirmPassword: passwordSchema,
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: formErrorText.confirmPassword,
    path: ["confirmPassword"],
  });

export const EditUserSchema = z
  .object({
    id: z.coerce.number(),
    isActive: z.boolean().optional(),
    firstName: firstNameSchema.optional(),
    lastName: lastNameSchema.optional(),
    email: emailSchema.optional(),
    roleId: z.coerce.number().min(1, formErrorText.role),
    password: passwordSchema.optional().or(z.literal("")),
    confirmPassword: passwordSchema.optional().or(z.literal("")),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: formErrorText.confirmPassword,
    path: ["confirmPassword"],
  });

export const UpdatePasswordSchema = z
  .object({
    id: z.coerce.number().optional(),
    email: emailSchema.optional(),
    currentPassword: passwordSchema,
    newPassword: passwordSchema,
    confirmPassword: passwordSchema,
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: formErrorText.confirmPassword,
    path: ["confirmPassword"],
  });
