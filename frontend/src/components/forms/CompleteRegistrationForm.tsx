"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { CalendarIcon } from "@radix-ui/react-icons";
import {
  Popover,
  PopoverTrigger,
  PopoverContent,
} from "~/components/ui/popover";
import { type Control, useForm } from "react-hook-form";
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
import { cn } from "~/lib/utils";
import { Calendar } from "../ui/calendar";
import { format } from "date-fns";
import { useMutation } from "@tanstack/react-query";
import { API_BASE_URL, TOKEN_KEY } from "~/lib/consts";
import axios from "axios";
import { type IAuthResponse } from "~/responses/IAuthResponse";

const formSchema = z.object({
  FirstName: z.string().min(2),
  LastName: z.string().min(2),
  DateOfBirth: z.date(),
  DateOfLicenseObtained: z.date(),
  Location: z.string().min(2),
});

type FormSchemaType = z.infer<typeof formSchema>;

const CompleteRegistrationDatePicker = ({
  formControl,
  name,
  label,
}: {
  formControl: Control<FormSchemaType>;
  name: "DateOfBirth" | "DateOfLicenseObtained";
  label: string;
}) => {
  return (
    <FormField
      control={formControl}
      name={name}
      render={({ field }) => (
        <FormItem className="flex flex-col">
          <FormLabel>{label}</FormLabel>
          <Popover>
            <PopoverTrigger asChild>
              <FormControl>
                <Button
                  variant={"outline"}
                  className={cn(
                    "w-full pl-3 text-left font-normal",
                    !field.value && "text-muted-foreground",
                  )}
                >
                  {field.value && format(field.value, "PPP")}
                  <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                </Button>
              </FormControl>
            </PopoverTrigger>
            <PopoverContent className="w-auto p-0" align="start">
              <Calendar
                mode="single"
                selected={field.value}
                onSelect={field.onChange}
                disabled={(date) =>
                  date > new Date() || date < new Date("1900-01-01")
                }
                initialFocus
              />
            </PopoverContent>
          </Popover>
          <FormMessage />
        </FormItem>
      )}
    />
  );
};

const CompleteRegistrationForm = () => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      FirstName: "",
      LastName: "",
      DateOfBirth: undefined,
      DateOfLicenseObtained: undefined,
      Location: "",
    },
  });

  const mutation = useMutation({
    mutationFn: async (values: FormSchemaType) => {
      const payload = {
        ...values,
        DateOfBirth: values.DateOfBirth.toISOString(),
        DateOfLicenseObtained: values.DateOfLicenseObtained.toISOString(),
      };
      const url = `${API_BASE_URL}/Auth/finish-registration`;
      const token = localStorage.getItem(TOKEN_KEY);
      const response = await axios.post<IAuthResponse>(url, payload, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });
      return response.data;
    },
    onSuccess: (data) => {
      localStorage.setItem(TOKEN_KEY, data.token);
      if (data.finishRegistration) {
        console.error("Reigstration failed. Try again later.");
      }
    },
    onError: (error) => {
      console.error("Request failed:", error);
    },
  });

  function onSubmit(values: FormSchemaType) {
    mutation.mutate(values);
    console.log("Successfully logged in");
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="w-full space-y-8">
        <FormField
          control={form.control}
          name="FirstName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>First Name</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="LastName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Last Name</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="Location"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Location</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <CompleteRegistrationDatePicker
          formControl={form.control}
          name="DateOfBirth"
          label="Date of birth"
        />
        <CompleteRegistrationDatePicker
          formControl={form.control}
          name="DateOfLicenseObtained"
          label="Date of license obtained"
        />
        <Button type="submit">Create profile</Button>
      </form>
    </Form>
  );
};

export default CompleteRegistrationForm;