using JsonApiSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PatreonNet.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JsonApiSerializer.JsonApi;
using JsonApiSerializer.JsonApi.WellKnown;
using Newtonsoft.Json.Linq;

namespace PatreonNet.Tests
{
    [TestClass]
    public class DeserializeTest
    {
        [TestMethod]
        public void IdentityDeserializeTest()
        {
            //Prepare
            User user;

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("PatreonNet.Tests.Resources.Identity.json"))
            using (var reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();

                //Test
                user = JsonConvert.DeserializeObject<User>(json, new JsonApiSerializerSettings());
            }

            //Assert
            Assert.IsNotNull(user);
            Assert.AreEqual("12345", user.Id);
            Assert.AreEqual("A Patreon Platform User", user.About);
            Assert.AreEqual(new DateTimeOffset(2018, 04, 01, 0, 36, 26, TimeSpan.Zero), user.Created);
            Assert.AreEqual("platform@patreon.com", user.Email);
            Assert.AreEqual("Platform", user.FirstName);
            Assert.AreEqual("Platform Team", user.FullName);
            Assert.AreEqual(@"https://url.example", user.ImageUrl);
            Assert.AreEqual("Platform", user.LastName);
            Assert.AreEqual("https://url.example", user.ThumbUrl);
            Assert.AreEqual("https://www.patreon.com/example", user.Url);
        }

        [TestMethod]
        public void MemberDeserializeTest()
        { //Prepare
            Member member;

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("PatreonNet.Tests.Resources.Member.json"))
            using (var reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();

                member = JsonConvert.DeserializeObject<Member>(json, new JsonApiSerializerSettings());
            }

            //Assert
            Assert.IsNotNull(member);
            Assert.AreEqual("03ca69c3-ebea-4b9a-8fac-e4a837873254", member.Id);
            Assert.AreEqual("Platform Team", member.FullName);
            Assert.AreEqual("\"platform@team.com,", member.Email);
            Assert.AreEqual(false, member.IsFollower);
            Assert.AreEqual(new DateTimeOffset(2018, 04, 01, 21, 28, 06, TimeSpan.Zero), member.LastChargeDate);
            Assert.AreEqual("Paid", member.LastChargeStatus);
            Assert.AreEqual(400, member.LifetimeSupportCents);
            Assert.AreEqual("active_patron", member.PatronStatus);
            Assert.AreEqual(100, member.CurrentlyEntitledAmountCents);
            Assert.IsTrue(member.PledgeRelationshipStart.HasValue);
            Assert.AreEqual(636581972078614050, member.PledgeRelationshipStart.Value.UtcTicks);
            Assert.AreEqual(100, member.WillPayAmountCents);

            Assert.IsNotNull(member.Address);
            Assert.AreEqual("123456", member.Address.Id);
            Assert.AreEqual("Platform Team", member.Address.Addressee);
            Assert.AreEqual("San Francisco", member.Address.City);
            Assert.AreEqual(true, member.Address.Confirmed);
            Assert.IsNull(member.Address.ConfirmedAt);
            Assert.AreEqual("US", member.Address.Country);
            Assert.AreEqual(new DateTimeOffset(2018, 06, 03, 16, 23, 38, TimeSpan.Zero), member.Address.CreatedAt);
            Assert.AreEqual("555 Main St", member.Address.Line1);
            Assert.AreEqual(string.Empty, member.Address.Line2);
            Assert.IsNull(member.Address.PhoneNumber);
            Assert.AreEqual("94103", member.Address.PostalCode);
            Assert.AreEqual("CA", member.Address.State);

            Assert.IsNotNull(member.CurrentlyEntitledTiers);
            Assert.AreEqual(1, member.CurrentlyEntitledTiers.Count);
            Assert.AreEqual("99001122", member.CurrentlyEntitledTiers[0].Id);
            Assert.AreEqual("Tshirt Tier", member.CurrentlyEntitledTiers[0].Title);

            Assert.IsNotNull(member.User);
            Assert.AreEqual("654321", member.User.Id);
            Assert.IsInstanceOfType(member.User, typeof(User));
            Assert.AreEqual("Platform Team", member.FullName);
        }

        [TestMethod]
        public void CampaignDeserializeTest()
        {
            //Prepare
            DocumentRoot<List<Campaign>> root;

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("PatreonNet.Tests.Resources.Campaign.json"))
            using (var reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();

                root = JsonConvert.DeserializeObject<DocumentRoot<List<Campaign>>>(json, new JsonApiSerializerSettings());
            }

            //Assert
            Assert.IsNotNull(root);
            Assert.IsTrue(root.Meta.ContainsKey("pagination"));

            Assert.IsNotNull(root.Data);

            List<Campaign> campaigns = root.Data;

            Assert.AreEqual(1, campaigns.Count);
            Assert.AreEqual("1234560", campaigns[0].Id);
            Assert.AreEqual(new DateTimeOffset(2018, 05, 04, 23, 34, 08, TimeSpan.Zero), campaigns[0].CreatedAt);
            Assert.AreEqual("online communities", campaigns[0].CreationName);
            Assert.AreEqual("1234567890", campaigns[0].DiscordServerId);
            Assert.AreEqual("1234567890", campaigns[0].GoogleAnalyticsId);
            Assert.IsTrue(campaigns[0].HasRss);
            Assert.IsTrue(campaigns[0].HasSentRssNotify);
            Assert.AreEqual(@"https://example.url", campaigns[0].ImageSmallUrl);
            Assert.IsTrue(campaigns[0].IsChargedImmediately.HasValue);
            Assert.IsFalse(campaigns[0].IsChargedImmediately.Value);
            Assert.IsFalse(campaigns[0].IsMonthly);
            Assert.IsFalse(campaigns[0].IsNsfw);
            Assert.IsNull(campaigns[0].MainVideoEmbed);
            Assert.AreEqual(@"https://example.url", campaigns[0].MainVideoUrl);
            Assert.IsNull(campaigns[0].OneLiner);
            Assert.AreEqual(2, campaigns[0].PatronCount);
            Assert.AreEqual("creation", campaigns[0].PayPerName);
            Assert.AreEqual(@"/bePatron?c=1234560", campaigns[0].PledgeUrl);
            Assert.AreEqual(new DateTimeOffset(2018, 05, 09, 17, 12, 01, TimeSpan.Zero), campaigns[0].PublishedAt);
            Assert.AreEqual("https://example.url", campaigns[0].RssArtworkUrl);
            Assert.AreEqual("My custom feed", campaigns[0].RssFeedTitle);
            Assert.AreEqual("Putting the internet to work for creators.", campaigns[0].Summary);
            Assert.IsNull(campaigns[0].ThanksEmbed);
            Assert.IsNull(campaigns[0].ThanksMessage);
            Assert.IsNull(campaigns[0].ThanksVideoUrl);
        }
    }
}
