"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Button } from "~/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "~/components/ui/form";
import { Input } from "~/components/ui/input";
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardFooter,
} from "~/components/ui/card";
import { useMutation } from "@tanstack/react-query";
import { useToast } from "~/hooks/use-toast";
import { useRouter } from "next/navigation";
import { Loader2 } from "lucide-react";
import { type ILoginEmployeeProps, loginEmployee } from "~/api/loginEmployee";
import { EMPLOYEE_TOKEN_KEY } from "~/lib/consts";

const formSchema = z.object({
  UserName: z.string().min(1),
  Password: z.string().min(1),
});
type FormSchemaType = z.infer<typeof formSchema>;

const EmployeeLoginForm = () => {
  const router = useRouter();
  const { toast } = useToast();
  const form = useForm<FormSchemaType>({
    defaultValues: {
      UserName: "",
      Password: "",
    },
    resolver: zodResolver(formSchema),
  });

  const mutation = useMutation({
    mutationFn: (values: ILoginEmployeeProps) => loginEmployee(values),
    onSuccess: (data) => {
      sessionStorage.setItem(EMPLOYEE_TOKEN_KEY, data.token);
      router.push("/employee");
    },
    onError: (error) => {
      toast({
        title: "Failed to login!",
        description: `An error occured while trying to login as employee (${error.message}). Try again later.`,
        variant: "destructive",
      });
    },
  });

  const onSubmit = (values: FormSchemaType) => {
    mutation.mutate({ password: values.Password, username: values.UserName });
  };

  return (
    <Card className="w-full max-w-md">
      <CardHeader className="space-y-1">
        <CardTitle className="text-center text-2xl font-bold">
          Employee Login
        </CardTitle>
      </CardHeader>
      <CardContent>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <FormField
              control={form.control}
              name="UserName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Username</FormLabel>
                  <FormControl>
                    <Input placeholder="Enter your username" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="Password"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Password</FormLabel>
                  <FormControl>
                    <Input
                      type="password"
                      placeholder="Enter your password"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <CardFooter className="px-0">
              <Button
                type="submit"
                className="w-full"
                disabled={mutation.isPending}
              >
                {mutation.isPending && <Loader2 className="animate-spin" />}
                Login
              </Button>
            </CardFooter>
          </form>
        </Form>
      </CardContent>
    </Card>
  );
};

export default EmployeeLoginForm;
