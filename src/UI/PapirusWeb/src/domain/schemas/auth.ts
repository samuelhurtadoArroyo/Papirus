import { z } from "zod";
import { emailRegex, passwordRegex } from "./common";
import { textConstants } from "../globalization/es";

const formErrorText = textConstants.components.usersForm.error;

export const LoginSchema = z
	.object({
		email: z.string().regex(emailRegex, formErrorText.email),
		password: z.string().regex(passwordRegex, formErrorText.password)
	})
