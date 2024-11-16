"use client";

import {
  Dialog,
  DialogTrigger,
  DialogContent,
  DialogTitle,
  DialogDescription,
  DialogHeader,
} from "./ui/dialog";
import { Button } from "./ui/button";
import GoogleAuth from "./GoogleAuth";

const LoginDialogButton = () => {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="ghost">Login</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-sm">
        <DialogHeader>
          <DialogTitle>Login to your account</DialogTitle>
          <DialogDescription className="py-3">
            <GoogleAuth />
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};

export default LoginDialogButton;
