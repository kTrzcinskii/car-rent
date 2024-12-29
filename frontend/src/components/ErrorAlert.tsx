import React from "react";
import { Alert, AlertTitle, AlertDescription } from "../components/ui/alert";
import { XCircle } from "lucide-react";

interface IErrorAlertProps {
  title: string;
  message: string;
}

const ErrorAlert = (props: IErrorAlertProps) => {
  return (
    <Alert variant="destructive">
      <XCircle className="h-5 w-5 text-red-500" />
      <AlertTitle>{props.title}</AlertTitle>
      <AlertDescription>{props.message}</AlertDescription>
    </Alert>
  );
};

export default ErrorAlert;
