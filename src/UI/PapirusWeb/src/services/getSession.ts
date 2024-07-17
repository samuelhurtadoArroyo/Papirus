import { auth } from "@/app/api/auth/[...nextauth]/auth";
import { cache } from "react";

export default cache(auth);
