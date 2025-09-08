import { Navigate } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";
import ProfileForm from "../../components/auth/ProfileForm";
import MainLayout from "../../common/layouts/MainLayout";

const ProfilePage = () => {
  const { user, loading } = useAuth();

  if (loading) {
    return (
      <MainLayout>
        <div className="flex justify-center items-center min-h-screen">
          <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-500"></div>
        </div>
      </MainLayout>
    );
  }

  if (!user) {
    return <Navigate to="/login" replace />;
  }

  return (
    <MainLayout>
      <div className="py-8">
        <h1 className="text-3xl font-bold text-center mb-8">Profile</h1>
        <ProfileForm />
      </div>
    </MainLayout>
  );
};

export default ProfilePage;
