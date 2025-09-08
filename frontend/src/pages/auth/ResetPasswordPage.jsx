import ResetPasswordForm from "../../components/auth/ResetPasswordForm";
import MainLayout from "../../common/layouts/MainLayout";

const ResetPasswordPage = () => {
  return (
    <MainLayout>
      <div className="py-8">
        <h1 className="text-3xl font-bold text-center mb-8">Reset Password</h1>
        <ResetPasswordForm />
      </div>
    </MainLayout>
  );
};

export default ResetPasswordPage;
