import { z } from "zod";

export const GuardianshipSchema = z.object({
  id: z.coerce.number(),
  status: z.string(),
});
